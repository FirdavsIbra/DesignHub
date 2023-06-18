using Domain.Models;
using Domain.Repositories;
using Domain.Services;

namespace Service.Services
{
    public class DesignService : IDesignService
    {
        private readonly IDesignRepository _designRepository;
        public DesignService(IDesignRepository designRepository)
        {
            _designRepository = designRepository;
        }

        public async Task AddOrUpdateDesignAsync(IDesign design)
        {
            var designs = await _designRepository.GetAllAsync();
            var existingDesign = designs.FirstOrDefault(x => x.UserId == design.UserId);
            if(existingDesign != null) 
            {
                design.Id = existingDesign.Id;
                await _designRepository.UpdateAsync(design);
            }
            else
            {
                await _designRepository.AddAsync(design);
            }
        }

        public async Task<IDesign> GetByUserIdAsync(int userId)
        {
            return await _designRepository.GetByUserIdAsync(userId);
        }
    }
}
