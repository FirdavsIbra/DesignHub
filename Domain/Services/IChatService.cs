using Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Domain.Services
{
    public interface IChatService
    {
        public Task SendMessage(IChatMessage message);
        public Task<IChatMessage[]> GetAllMessagesByRecieverId(int id);
        public Task<IChatMessage[]> GetAllMessagesAsync();
        public Task<IChatMessage[]> GetAllMessagesByRecieverAndSenderId(int recieverId, int senderID);
    }
}