// AttachmentManager.cs
using BusnissLayer.DTOs.AttachmentDtos;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using SchoolApp.BL.Dtos.Common;
using System.IO;
using System.Linq;
namespace BusnissLayer.Managers
{
    public class AttachmentManager : IAttachmentManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _uploadPath = "Uploads";

        public AttachmentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public async Task<GeneralResult<AttachmentDto>> UploadAttachment(int bugId, IFormFile file)
        {
            try
            {

                var bug = await _unitOfWork.BugRepository.GetByIdAsync(b=> b.Id==bugId);
                if (bug == null)
                {
                    return new GeneralResult<AttachmentDto>
                    {
                        IsValid = false,
                        Errors = [new() { Code = "NOT_FOUND", Message = "Bug not found" }]
                    };
                }

                // 2. حفظ الملف على السيرفر
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(_uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var attachment = new Attachment
                {
                    FileName = file.FileName,
                    FileType = Path.GetExtension(file.FileName),
                    ContentType = file.ContentType,
                    FilePath = filePath,
                    BugId = bugId
                };

                await _unitOfWork.AttachmentRepository.Add(attachment);
                await _unitOfWork.SaveChangesAsync();

                return new GeneralResult<AttachmentDto>
                {
                    IsValid = true,
                    Data = new AttachmentDto
                    {
                        Id = attachment.Id,
                        FileName = attachment.FileName,
                        FileType = attachment.FileType,
                        ContentType = attachment.ContentType,
                        FilePath = attachment.FilePath,
                        CreatedAt = attachment.CreatedAt,
                        BugId = attachment.BugId
                    }
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<AttachmentDto>
                {
                    IsValid = false,
                    Errors = [new() { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<List<AttachmentDto>>> GetAttachments(int bugId)
        {
            try
            {
                var allAttachments = await _unitOfWork.AttachmentRepository.GetAllAsync();
                var attachments = allAttachments.Where(a => a.BugId == bugId).ToList();


                var result = attachments.Select(a => new AttachmentDto
                {
                    Id = a.Id,
                    FileName = a.FileName,
                    FileType = a.FileType,
                    ContentType = a.ContentType,
                    FilePath = a.FilePath,
                    CreatedAt = a.CreatedAt,
                    BugId = a.BugId
                }).ToList();

                return new GeneralResult<List<AttachmentDto>>
                {
                    IsValid = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<List<AttachmentDto>>
                {
                    IsValid = false,
                    Errors = [new() { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<bool>> DeleteAttachment(int attachmentId)
        {
            try
            {
                var attachment = await _unitOfWork.AttachmentRepository.GetByIdAsync(a=> a.Id==attachmentId);
                if (attachment == null)
                {
                    return new GeneralResult<bool>
                    {
                        IsValid = false,
                        Errors = [new() { Code = "NOT_FOUND", Message = "Attachment not found" }]
                    };
                }

                if (File.Exists(attachment.FilePath))
                {
                    File.Delete(attachment.FilePath);
                }

                _unitOfWork.AttachmentRepository.Delete(attachment);
                await _unitOfWork.SaveChangesAsync();

                return new GeneralResult<bool>
                {
                    IsValid = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<bool>
                {
                    IsValid = false,
                    Errors = [new() { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<DownloadAttachmentDto>> DownloadAttachment(int attachmentId)
        {
            try
            {
                var attachment = await _unitOfWork.AttachmentRepository.GetByIdAsync(a=> a.Id==attachmentId);
                if (attachment == null || !File.Exists(attachment.FilePath))
                {
                    return new GeneralResult<DownloadAttachmentDto>
                    {
                        IsValid = false,
                        Errors = [new() { Code = "NOT_FOUND", Message = "Attachment not found" }]
                    };
                }

                var fileBytes = await File.ReadAllBytesAsync(attachment.FilePath);

                return new GeneralResult<DownloadAttachmentDto>
                {
                    IsValid = true,
                    Data = new DownloadAttachmentDto
                    {
                        FileBytes = fileBytes,
                        ContentType = attachment.ContentType ?? "application/octet-stream",
                        FileName = attachment.FileName
                    }
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<DownloadAttachmentDto>
                {
                    IsValid = false,
                    Errors = [new() { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }
    }
}