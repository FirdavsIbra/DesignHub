using Domain.Models;

namespace Domain.Repositories
{
    public interface ISketchRepository
    {
        public Task AddAsync(ISketch sketch);

        public Task<ISketch> GetByUserId(int userId);

        public Task<ISketch[]> GetAllAsync();

        public Task Update(ISketch sketch);
    }
}
