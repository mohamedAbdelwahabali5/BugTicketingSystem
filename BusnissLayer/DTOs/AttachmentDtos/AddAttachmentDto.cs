using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.DTOs.AttachmentDtos
{
    public class AddAttachmentDto
    {
        public required string FileName { get; set; }
        public required string FileType { get; set; }
        public string? ContentType { get; set; }
        public required string FilePath { get; set; }
        public int BugId { get; set; }
    }
}
