using Domain.Models;

namespace Domain.Services
{
    public interface ISketchService
    {
        public Task AddAsync(ISketch sketch);

        public Task<ISketch> GetByUserId(int userId);
    }
}
