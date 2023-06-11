using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Service.DTO;

namespace Service.Services
{
    public class DesignService : IDesignService
    {
        private readonly IDesignRepository _designRepository;

        public DesignService(IDesignRepository designRepository)
        {
            _designRepository = designRepository;
        }

        public async Task<int> UploadDesignAsync(IFormFile file)
        {
            var design = new DesignDTO
            {
                Title = "Design Title",
                Description = "Design Description"
            };

            await _designRepository.Add(design, file);

            return design.Id;
        }
    }
}
