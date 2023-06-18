using Domain.Models;

namespace Domain.Repositories
{
    public interface ISketchRepository
    {
        public Task AddAsync(ISketch sketch);

        public Task<ISketch> GetByUserIdAsync(int userId);

        public Task<ISketch[]> GetAllAsync();

        public Task UpdateAsync(ISketch sketch);
    }
}
