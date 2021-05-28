using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.DTOs
{
    public class RefreshTokenDto
    {
        [Required]
        public String AccessToken { get; set; }
        [Required]
        public String RefreshToken { get; set; }
    }
}
