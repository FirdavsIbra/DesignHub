using Domain.Models;

namespace Domain.Repositories
{
    public interface IChatRepository
    {
        public Task<int> Create(IChatMessage message);
        public Task<IChatMessage> GetById(int id);
    }
}
