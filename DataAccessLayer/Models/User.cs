using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        //public required string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // relations with entities
        public int? ProjectId { get; set; }
        public Project? Project { get; set; } // Navigation property to the Project entity
        public ICollection<User_Bug>? User_Bugs { get; set; }

        public ICollection<Role>? Roles { get; set; } // Navigation property to the Role entity>
    }

}
