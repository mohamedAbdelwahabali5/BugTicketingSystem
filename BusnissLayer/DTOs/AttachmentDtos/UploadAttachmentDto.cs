using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.DTOs.AttachmentDtos
{
    public class UploadAttachmentDto
    {
        public IFormFile File { get; set; } = null!;
        public int BugId { get; set; }
    }
}
