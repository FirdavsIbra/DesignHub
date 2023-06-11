using Domain.Models;

namespace Domain.Services
{
    public interface IChatService
    {
        public Task<IChatMessage> GetChatMessages(int id);
        Task<bool> SendMessage(IChatMessage message);
        Task<bool> UploadFile(int companyId, byte[] fileData);
        public Task AddMessage(IChatMessage chatMessageDto);
    }
}
