using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyAsync(CompanyDTO companyDTO)
        {
            await _companyService.AddOrUpdateCompanyAsync(companyDTO);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(int userId)
        {
            return Ok(await _companyService.GetByUserIdAsync(userId));
        }

    }
}
