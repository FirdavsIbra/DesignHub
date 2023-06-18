using Domain.Models;

namespace Domain.Repositories
{
    public interface IChatRepository
    {
        public Task CreateAsync(IChatMessage message);
        public Task<IChatMessage> GetById(int id);
        public Task<IChatMessage[]> GetAllAsync();
        public Task<IChatMessage[]> GetAllMessagesByRecieverId(int id);
        public Task<IChatMessage[]> GetAllMessagesByRecieverAndSenderId(int recieverId, int senderID);
    }
}
