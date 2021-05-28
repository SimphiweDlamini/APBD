using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public String Login { get; set; }
        public String HashedPassword { get; set; }
        public byte[] Salt { get; set; }
        public String RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
       

    }
}
