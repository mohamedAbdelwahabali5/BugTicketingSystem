using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.DTOs.UserDtos
{
    public class TokenDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }

        public TokenDto() { }

        public TokenDto(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
  
}
