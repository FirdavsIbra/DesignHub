using Domain.Models;

namespace Domain.Repositories
{
    public interface IChatRepository
    {
        public Task<IChatMessage> GetById(int id);
        public Task Add(IChatMessage message);
        public Task Update(IChatMessage message);
        public Task Delete(int id);
        public Task<IEnumerable<IChatMessage>> GetMessagesByUserId(int userId);
    }
}
