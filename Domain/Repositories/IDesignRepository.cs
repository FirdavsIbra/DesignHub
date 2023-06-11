using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.Repositories
{
    public interface IDesignRepository
    {
        Task<IDesign> GetById(int id);
        public Task<IEnumerable<IDesign>> GetAll();
        public Task Add(IDesign design, IFormFile file);
        Task Update(IDesign design);
        Task Delete(int id);
    }
}
