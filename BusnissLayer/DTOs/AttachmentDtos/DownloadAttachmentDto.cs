using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.DTOs.AttachmentDtos
{
    public class DownloadAttachmentDto
    {
        public byte[] FileBytes { get; set; } = null!;
        public string ContentType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}
