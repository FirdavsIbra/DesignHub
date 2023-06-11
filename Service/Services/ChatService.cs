using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Service.DTO;

namespace Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileRepository _fileRepository;

        public ChatService(IChatRepository chatRepository, IAuthService authService, IHttpContextAccessor httpContextAccessor, IFileRepository fileRepository)
        {
            _chatRepository = chatRepository;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _fileRepository = fileRepository;
        }

        public async Task<IChatMessage> GetChatMessages(int id)
        {
            var chatMessageEntity = await _chatRepository.GetById(id);
            return chatMessageEntity;
        }

        public async Task<IEnumerable<IChatMessage>> GetMessagesForUser(int userId)
        {
            var chatMessageEntities = await _chatRepository.GetMessagesByUserId(userId);
            return chatMessageEntities;
        }

        public async Task AddMessage(IChatMessage chatMessageDto)
        {
            var senderId = _authService.GetCurrentUserId(_httpContextAccessor.HttpContext.User);
            var receiverId = chatMessageDto.RecieverId;

            var chatMessage = new ChatMessageDTO
            {
                SenderId = senderId,
                RecieverId = receiverId,
                Message = chatMessageDto.Message,
                MediaUrl = chatMessageDto.MediaUrl,
                Timestamp = DateTime.Now
            };

            await _chatRepository.Add(chatMessage);
        }

        public async Task UpdateMessage(int messageId, ChatMessageDTO chatMessageDto)
        {
            var chatMessage = await _chatRepository.GetById(messageId);
            if (chatMessage == null)
                throw new Exception("Chat message not found");

            chatMessage.Message = chatMessageDto.Message;
            chatMessage.MediaUrl = chatMessageDto.MediaUrl;

            await _chatRepository.Update(chatMessage);
        }

        public async Task DeleteMessage(int messageId)
        {
            var chatMessage = await _chatRepository.GetById(messageId);
            if (chatMessage == null)
                throw new Exception("Chat message not found");

            await _chatRepository.Delete(messageId);
        }

        public async Task<bool> SendMessage(IChatMessage message)
        {
            try
            {
                await AddMessage(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UploadFile(int companyId, byte[] fileData)
        {
            try
            {
                var fileEntity = new FileEntityDTO
                {
                    CompanyId = companyId,
                    FileData = fileData
                };

                await _fileRepository.Add(fileEntity); 

                return true; 
            }
            catch
            {
                return false;
            }
        }
    }
}
