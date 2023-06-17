using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.Repositories
{
    public interface IDesignRepository
    {
        Task<IDesign> GetByUserId(int id);
        public Task<IDesign[]> GetAllAsync();
        public Task Add(IDesign design);
        Task Update(IDesign design);
    }
}
