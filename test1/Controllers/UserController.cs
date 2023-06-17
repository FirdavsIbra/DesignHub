using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsernameAsync(int id)
        {
            var username = await _userService.GetUsernameAsync(id);
            return Ok(new
            {
                name = username
            });
        }
    }
}
