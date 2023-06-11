using API.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace test1.Controllers
{
        [ApiController]
        [Route("api/auth")]
        public class AuthController : ControllerBase
        {
            private readonly IAuthService _authService;

            public AuthController(IAuthService authService)
            {
                _authService = authService;
            }

            [HttpPost("register")]
            public IActionResult Register([FromQuery] RegisterRequestModel model)
            {
                // Проверка валидности модели

                if (_authService.RegisterUser(model.Username, model.Password) != null)
                {
                    return Ok("Registration successfully");
                }
                else
                {
                    return BadRequest("Registration failed");
                }
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login(LoginRequestModel model)
            {
                // Проверка валидности модели

                if (await _authService.Login(model.Username, model.Password))
                {
                    string token = _authService.GenerateJwtToken(model.Username);
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized("Invalid username or password");
                }
            }
        }
    }
