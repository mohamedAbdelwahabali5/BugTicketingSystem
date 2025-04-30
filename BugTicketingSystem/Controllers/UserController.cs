using BusnissLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BusnissLayer.DTOs.UserDtos;

namespace BugTicketingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userManager.GetAllUsers();
            return result.IsValid ? Ok(result.Data) : BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserLogDto userLogDto)
        {
            if (userLogDto == null)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await _userManager.LoginUser(userLogDto);
            return result.IsValid ? Ok(result.Data) : Unauthorized(result.Errors);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserRegDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await _userManager.RegisterUser(userDto);
            return result.IsValid ? Ok(result.Data) : BadRequest(result.Errors);
        }
    }
}