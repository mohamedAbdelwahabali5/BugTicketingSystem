using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusnissLayer.Managers;
namespace BugTicketingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentManager _attachmentManager;

        public AttachmentController(IAttachmentManager attachmentManager)
        {
            _attachmentManager = attachmentManager;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int bugId, IFormFile file)
        {
            var result = await _attachmentManager.UploadAttachment(bugId, file);
            return result.IsValid ? Ok(result.Data) : BadRequest(result.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetAttachments(int bugId)
        {
            var result = await _attachmentManager.GetAttachments(bugId);
            return result.IsValid ? Ok(result.Data) : BadRequest(result.Errors);
        }

        [HttpDelete("{attachmentId}")]
        public async Task<IActionResult> Delete(int bugId, int attachmentId)
        {
            var result = await _attachmentManager.DeleteAttachment(attachmentId);
            return result.IsValid ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{attachmentId}/download")]
        public async Task<IActionResult> Download(int bugId, int attachmentId)
        {
            var result = await _attachmentManager.DownloadAttachment(attachmentId);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            return File(
                result!.Data.FileBytes,
                result!.Data.ContentType,
                result!.Data.FileName);
        }
    }

}
