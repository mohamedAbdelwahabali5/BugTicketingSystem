using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.DTOs.ProjectDtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<PBug>? Bugs { get; set; } = new List<PBug>();
    }

    public class PBug
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public BugStatus Status { get; set; }
        public BugPriority Priority { get; set; }
    }
}
