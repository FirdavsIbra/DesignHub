using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Service.DTO;
using System.Security.Claims;

namespace Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatMessageRepository;
        private readonly IAuthService _authService;

        public ChatService(IChatRepository chatMessageRepository, IAuthService authService)
        {
            _chatMessageRepository = chatMessageRepository ?? throw new ArgumentNullException(nameof(chatMessageRepository));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<int> SendMessage(string message, IFormFile mediaFile, ClaimsPrincipal user)
        {
            int currentUserId = await _authService.GetCurrentUserId(user);

            var chatMessage = new ChatMessageDTO
            {
                SenderId = currentUserId,
                Message = message,
                Timestamp = DateTime.UtcNow
            };
                
            if (mediaFile != null)
            {
                string mediaUrl = await SaveMediaFile(mediaFile);
                chatMessage.MediaUrl = mediaUrl;
            }

            return await _chatMessageRepository.Create(chatMessage);
        }

        public async Task<string> SaveMediaFile(IFormFile mediaFile)
        {
            if (mediaFile == null || mediaFile.Length == 0)
            {
                throw new ArgumentException("Media file is null or empty.");
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(mediaFile.FileName);

            string mediaFolderPath = "path_to_save_files";

            string filePath = Path.Combine(mediaFolderPath, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await mediaFile.CopyToAsync(stream);
                }

                string baseUrl = "base_url_for_server";
                string mediaUrl = Path.Combine(baseUrl, fileName);

                return mediaUrl;
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving media file: " + ex.Message);
            }
        }
    }
}
