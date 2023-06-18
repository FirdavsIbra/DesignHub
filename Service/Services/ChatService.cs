using Domain.Models;
using Domain.Repositories;
using Domain.Services;

namespace Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatMessageRepository;

        public ChatService(IChatRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<IChatMessage[]> GetAllMessagesAsync()
        {
            return await _chatMessageRepository.GetAllAsync();
        }

        public async Task SendMessage(IChatMessage message)
        {
            await _chatMessageRepository.CreateAsync(message);
        }
        public async Task<IChatMessage[]> GetAllMessagesByRecieverId(int id)
        {
            return await _chatMessageRepository.GetAllMessagesByRecieverId(id);
        }

        public async Task<IChatMessage[]> GetAllMessagesByRecieverAndSenderId(int recieverId, int senderId)
        {
            return await _chatMessageRepository.GetAllMessagesByRecieverAndSenderId(recieverId, senderId);
        }
    }
}
