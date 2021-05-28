using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.DTOs
{
    public class UserDto
    {
        [Required]
        public String Login { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
