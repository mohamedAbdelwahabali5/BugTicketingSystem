using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusnissLayer.Managers;
using Microsoft.AspNetCore.Authorization;
using BusnissLayer.DTOs.BugDtos;


namespace BugTicketingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly IBugManager _bugManager;

        public BugController(IBugManager bugManager)
        {
            _bugManager = bugManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllBugs()
        {
            var result = await _bugManager.GetAllBugs();
            return result.IsValid ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBugById(int id)
        {
            var result = await _bugManager.GetBugById(id);
            return result!.IsValid ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBug(AddBugDto bugDto)
        {
            var result = await _bugManager.CreateBug(bugDto);
            return result!.IsValid ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateBug([FromRoute] int id, UpdateBugDto bugDto)
        {
            var result = await _bugManager.UpdateBug(id, bugDto);
            return result!.IsValid ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteBug([FromRoute] int id)
        {
            var result = await _bugManager.DeleteBug(id);
            return result!.IsValid ? Ok(result) : BadRequest(result);
        }
    }
}
