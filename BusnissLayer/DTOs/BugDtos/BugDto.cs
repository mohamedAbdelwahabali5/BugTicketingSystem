using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.DTOs.BugDtos
{
    public class BugDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public BugStatus Status { get; set; }
        public BugPriority Priority { get; set; }
        public ICollection<BAttachmentDto>? Attachments { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class BAttachmentDto
    {
        public required string FileName { get; set; }
        public required string FileType { get; set; }
        public string? ContentType { get; set; }
        public required string FilePath { get; set; }
    }
}
