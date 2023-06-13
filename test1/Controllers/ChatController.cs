﻿using API.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("message")]
        public async Task<IActionResult> SendMessage([FromForm] ChatMessageModel request, [FromForm] IFormFile mediaFile)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Message))
                {
                    return BadRequest("Message text is required.");
                }

                // Get the user's ID from the token
                int currentUserId = await _authService.GetCurrentUserId(User);

                // Check if the user is registered
                bool isUserRegistered = currentUserId != 0;
                if (!isUserRegistered)
                {
                    return BadRequest("User is not registered.");
                }

                await _chatService.SendMessage(request.Message, mediaFile, User);

                return Ok("Message sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending message: " + ex.Message);
            }
        }
    }
}