using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/design")]
    public class DesignController : ControllerBase
    {
        private readonly IDesignService _designService;

        public DesignController(IDesignService designService)
        {
            _designService = designService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyAsync(DesignDTO designDTO)
        {
            await _designService.AddOrUpdateDesignAsync(designDTO);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(int userId)
        {
            return Ok(await _designService.GetByUserIdAsync(userId));
        }

    }
}
