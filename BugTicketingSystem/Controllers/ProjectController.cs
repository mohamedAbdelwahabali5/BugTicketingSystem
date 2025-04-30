using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusnissLayer.Managers;
using BusnissLayer.DTOs.ProjectDtos;
using Microsoft.AspNetCore.Authorization;
namespace BugTicketingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly IProjectManager _projectManager;

        public ProjectController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var result = await _projectManager.GetAllProjects();
            return result.IsValid ? Ok(result) : BadRequest(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var result = await _projectManager.GetProjectById(id);
            return result.IsValid ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProject(AddProjectDto projectDto)
        {
            var result = await _projectManager.AddProject(projectDto);
            return result.IsValid ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] int id, UpdatedProjectDto projectDto)
        {
            var result = await _projectManager.UpdateProject(projectDto, id);
            return result.IsValid ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            var result = await _projectManager.DeleteProject(id);
            return result.IsValid ? Ok(result) : BadRequest(result);
        }
    }
}
