using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public enum BugStatus { Open, InProgress, Fixed, Closed }
    public enum BugPriority { Low, Medium, High, Critical }
    public class Bug
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public BugStatus Status { get; set; }
        public BugPriority Priority { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // relations with entities
        public ICollection<User_Bug>? User_Bugs { get; set; }
        public ICollection<Attachment>? Attachments { get; set; } 
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

    }

}
