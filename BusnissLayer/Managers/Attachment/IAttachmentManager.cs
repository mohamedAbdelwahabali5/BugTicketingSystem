using BusnissLayer.DTOs.AttachmentDtos;
using BusnissLayer.DTOs.ProjectDtos;
using Microsoft.AspNetCore.Http;
using SchoolApp.BL.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.Managers
{
    public interface IAttachmentManager
    {
        Task<GeneralResult<AttachmentDto>> UploadAttachment(int bugId, IFormFile file);
        Task<GeneralResult<List<AttachmentDto>>> GetAttachments(int bugId);
        Task<GeneralResult<bool>> DeleteAttachment(int attachmentId);
        Task<GeneralResult<DownloadAttachmentDto>> DownloadAttachment(int attachmentId);
    }
}
