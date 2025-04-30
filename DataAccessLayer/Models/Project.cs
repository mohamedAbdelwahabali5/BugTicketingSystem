using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // relations with entities    
        public int? ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public  User? Manager { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Bug>? Bugs { get; set; }
    }

}
