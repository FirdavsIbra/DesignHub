using API.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IAuthService _authService;

        public ChatController(IChatService chatService, IAuthService authService)
        {
            _chatService = chatService;
            _authService = authService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendMessage(ChatMessageModel chatMessage)
        {
            int currentUserId = await _authService.GetCurrentUserIdAsync(User);

            ChatMessageDTO chatMessageDTO = new()
            {
                Message = chatMessage.Message,
                RecieverId = chatMessage.RecieverId,
                SenderId = currentUserId,
                DateTime = chatMessage.Date
            };

            await _chatService.SendMessage(chatMessageDTO);

            return Ok();
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetAllMessageAsync()
        {
            return Ok(await _chatService.GetAllMessagesAsync());
        }

        [HttpGet("messages/{id}")]
        public async Task<IActionResult> GetAllMessagesByRecieverId(int id)
        {
            return Ok(await _chatService.GetAllMessagesByRecieverId(id));
        }

        [HttpGet("messages/getByIds")]
        public async Task<IActionResult> GetAllMessagesByRecieverAndSenderid([FromQuery] int recieverId, int senderId)
        {
            return Ok(await _chatService.GetAllMessagesByRecieverAndSenderId(recieverId, senderId));
        }

    }
}
