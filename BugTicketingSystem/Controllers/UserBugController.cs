using BusnissLayer.Managers;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.Controllers
{
    [Route("api/bugs")]
    [ApiController]
    public class UserBugController : ControllerBase
    {
        private readonly IUserBugManager _userBugManager;

        public UserBugController(IUserBugManager userBugManager)
        {
            _userBugManager = userBugManager;
        }

        [HttpPost("{bugId}/assignees/{userId}")]
        public async Task<IActionResult> AssignUserToBug(int bugId, int userId)
        {
            var result = await _userBugManager.AssignUserToBug(bugId, userId);
            return result.IsValid ? Ok(result.Data) : BadRequest(result.Errors);
        }

        [HttpDelete("{bugId}/assignees/{userId}")]
        public async Task<IActionResult> RemoveUserFromBug(int bugId, int userId)
        {
            var result = await _userBugManager.RemoveUserFromBug(bugId, userId);
            return result.IsValid ? Ok(result.Data) : BadRequest(result.Errors);
        }
    }
}