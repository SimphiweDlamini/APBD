using _8_Tutorial.DTOs;
using _8_Tutorial.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthentictionService _authenticationService;
        public AccountsController(IAuthentictionService authenticationservice)
        {
            _authenticationService = authenticationservice;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto login) {
            if (!await _authenticationService.UserExist(login)) {
                return BadRequest("This User Does not exist");
            }
            return Ok(await _authenticationService.returnToken(login));
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto refreshTokenDto) {
            if (!await _authenticationService.DoesRefreshTokenMatch(refreshTokenDto))
            {
                return BadRequest("RefreshToken does not match");
            }
              
            else if (!_authenticationService.IsTokenValid(refreshTokenDto)) {
                return BadRequest("Token is Invalid");
            }
           
            return Ok(await _authenticationService.HasTokenExpired(refreshTokenDto));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDto newuser) {
            if (await _authenticationService.UserExist(newuser)) {
                return BadRequest("This User already exists in the system");
            }
           

            return Ok(await _authenticationService.addUser(newuser));
        }

    }
}
