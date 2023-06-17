using Domain.Models;

namespace Domain.Services
{
    public interface IDesignService
    {
        public Task AddOrUpdateDesignAsync(IDesign design);
        public Task<IDesign> GetByUserIdAsync(int userId);
    }
}