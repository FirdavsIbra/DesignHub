using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/sketches")]
    public class SketchController: ControllerBase
    {
        private readonly ISketchService _sketchService;
        public SketchController(ISketchService sketchService)
        {
            _sketchService = sketchService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(SketchDTO sketchDTO)
        {
            await _sketchService.AddAsync(sketchDTO);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(int userId)
        {
            return Ok(await _sketchService.GetByUserIdAsync(userId));
        }
    }
}
