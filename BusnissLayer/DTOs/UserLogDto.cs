using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.DTOs
{
    public class UserLogDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
