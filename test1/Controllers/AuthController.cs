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
            public async Task<IActionResult> Register([FromQuery] RegisterRequestModel model)
            {
                try
                {
                    await _authService.RegisterUser(model.Username, model.Password);
                }
                catch (Exception ex) 
                {
                    return BadRequest(ex.Message);
                
                }
                return Ok();
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login(LoginRequestModel model)
            {
                // Проверка валидности модели

                if (await _authService.Login(model.Username, model.Password))
                {
                    var user = await _authService.GetUserByUserNameAsync(model.Username);

                    string token = _authService.GenerateJwtToken(model.Username, user);
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized("Invalid username or password");
                }
            }
        }
    }
