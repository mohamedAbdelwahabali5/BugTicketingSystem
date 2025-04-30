using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public required string FileType { get; set; }
        public string? ContentType { get; set; }
        public required string FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // relationship
        public int BugId { get; set; }
        public  Bug? Bug { get; set; }

    }
}
