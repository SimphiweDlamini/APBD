using _8_Tutorial.DTOs;
using _8_Tutorial.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _8_Tutorial.Services
{
    public class AuthenicationService : IAuthentictionService
    {
        private readonly IConfiguration _configuration;
        private readonly DoctorDBContext _doctorDBContext;

        public AuthenicationService(IConfiguration configuration,DoctorDBContext doctorDBContext)
        {
            _configuration = configuration;
            _doctorDBContext = doctorDBContext;
                
                }

        public async Task<RefreshTokenDto> addUser(UserDto newuser)
        {
            var result = new RefreshTokenDto();
            var newid=0;
            if (!await _doctorDBContext.Users.AnyAsync())
            {
                newid = 0;
            }
            else { 
            newid = await _doctorDBContext.Users.MaxAsync(e => e.IdUser + 1);
             }
            var usetoadd = new User();
            var usersalt = GenerateSalt();
            var hashPassword = HashPassword(newuser.Password, usersalt);
            usetoadd.IdUser = newid;
            usetoadd.Login = newuser.Login;
            usetoadd.Salt = usersalt;
            usetoadd.HashedPassword = hashPassword;

            var accesstoken = GenerateToken(usetoadd);
            var refreshtoken = Guid.NewGuid();
            var expirationdate = accesstoken.ValidTo;
            usetoadd.RefreshToken = refreshtoken.ToString();
            usetoadd.ExpirationDate = expirationdate;

            result.AccessToken = new JwtSecurityTokenHandler().WriteToken(accesstoken);
            result.RefreshToken = refreshtoken.ToString();

            await _doctorDBContext.Users.AddAsync(usetoadd);
            await _doctorDBContext.SaveChangesAsync();

            return result;
        } 
            

        public async Task<bool> DoesRefreshTokenMatch(RefreshTokenDto refreshToken)
        {
            //check if user extracted from access token has matching refresh token in db
            var token = new JwtSecurityTokenHandler().ReadToken(refreshToken.AccessToken) as JwtSecurityToken;
            var allClaims = token.Claims.ToList();
            var login = allClaims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault();

            return await _doctorDBContext.Users.AnyAsync(e => e.Login == login.Value && refreshToken.RefreshToken == e.RefreshToken);
            
            
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[128];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public JwtSecurityToken GenerateToken(User user)
        {
            Claim[] claims =
                  {
                    new Claim(ClaimTypes.NameIdentifier,user.IdUser.ToString()),
                    new Claim(ClaimTypes.Name,user.Login)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://localhost:59831",
                audience: "http://localhost:59831",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials:creds
                );
            return token; 
        }

        public string HashPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                  password: password,
                  salt: salt,
                  prf: KeyDerivationPrf.HMACSHA1,
                   iterationCount: 10000,
                   numBytesRequested: 32
                ));

            return hashed;
        }

        public async Task<RefreshTokenDto> HasTokenExpired(RefreshTokenDto refreshToken)
        {
            var token = new JwtSecurityTokenHandler().ReadToken(refreshToken.AccessToken) as JwtSecurityToken;
            
            var expiration = token.Payload.ValidTo;
            if (expiration < DateTime.Now) {
                var allClaims = token.Claims.ToList();
                var login = allClaims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault();
                var idOfUser = allClaims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                var currentuser = await _doctorDBContext.Users.FirstOrDefaultAsync(e => e.Login == login.Value && e.IdUser == Int32.Parse(idOfUser.Value));
                var newaccesstoken = GenerateToken(currentuser);
                var newRefreshToken = Guid.NewGuid();
                //update the user
                //expirationdate of new token
                currentuser.ExpirationDate = newaccesstoken.ValidTo;
                currentuser.RefreshToken = newRefreshToken.ToString();
                // update details of the current refresh token
                refreshToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(newaccesstoken);
                refreshToken.RefreshToken = newRefreshToken.ToString();

                //make changes in db
                _doctorDBContext.Users.Update(currentuser);
                await _doctorDBContext.SaveChangesAsync();
            }

            return refreshToken;

        }

        public bool IsTokenValid(RefreshTokenDto refreshToken)
        {
            //check signature of token with the one used when it was generated

            var valparameters = new TokenValidationParameters {

                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret"]))
            };
            try
            {
                var som = new JwtSecurityTokenHandler().ValidateToken(refreshToken.AccessToken, valparameters, out SecurityToken securityToken);
            }
            catch {
                return false;
            }
            return true;
            
          
        }

        public async Task<RefreshTokenDto> returnToken(UserDto login)
        {
            var user = await _doctorDBContext.Users.FirstOrDefaultAsync(e => e.Login == login.Login);
            var refreshtoken = user.RefreshToken;
            var result = new RefreshTokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(GenerateToken(user)),
                RefreshToken = refreshtoken
            };

            return result;

        }

        public async Task<bool> UserExist(UserDto login)
        {
            if (!await _doctorDBContext.Users.AnyAsync(e => e.Login == login.Login)){
                return false;
            }
            var user = await _doctorDBContext.Users.FirstOrDefaultAsync(e => e.Login == login.Login);
            var hash = HashPassword(login.Password, user.Salt);
            if (user.HashedPassword != hash) {
                return false;
            }
            else {
                return true;
            }

        }
    }
}
