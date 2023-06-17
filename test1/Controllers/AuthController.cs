using API.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

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
            public async Task<IActionResult> Login([FromQuery] LoginRequestModel model)
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

            [HttpGet("getMe")]
            public async Task<IActionResult> GetMe()
            {
                // Получение токена из заголовка запроса
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                // Проверка валидности токена
                bool isValidToken = _authService.ValidateJwtToken(token);
                if (!isValidToken)
                {
                    return Unauthorized();
                }

                // Получение идентификатора пользователя из токена
                int userId = await _authService.GetCurrentUserId(User);
                if (userId == 0)
                {
                    return NotFound("User not found");
                }

                return Ok(new
                {
                    id = userId
                });
        }
        }
    }
