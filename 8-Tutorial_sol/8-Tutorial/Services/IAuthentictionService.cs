using _8_Tutorial.DTOs;
using _8_Tutorial.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Services
{
    public interface IAuthentictionService
    {
        public Task<bool> UserExist(UserDto login);
        public Task<RefreshTokenDto> returnToken(UserDto login);
        public Task<bool> DoesRefreshTokenMatch(RefreshTokenDto refreshToken);
        public bool IsTokenValid(RefreshTokenDto refreshToken);
        public JwtSecurityToken GenerateToken(User user);
        public String HashPassword(String password, byte[] salt);
        public Task<RefreshTokenDto> addUser(UserDto newuser);
        public byte[] GenerateSalt();
        public Task<RefreshTokenDto> HasTokenExpired(RefreshTokenDto refreshTokenDto);
    }
}
