using Domain.Models;

namespace Domain.Services
{
    public interface ISketchService
    {
        public Task AddAsync(ISketch sketch);
        public Task<ISketch> GetByUserIdAsync(int userId);
    }
}
