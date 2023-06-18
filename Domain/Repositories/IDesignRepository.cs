using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.Repositories
{
    public interface IDesignRepository
    {
        Task<IDesign> GetByUserIdAsync(int id);
        public Task<IDesign[]> GetAllAsync();
        public Task AddAsync(IDesign design);
        Task UpdateAsync(IDesign design);
    }
}
