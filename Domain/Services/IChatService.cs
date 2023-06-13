using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Domain.Services
{
    public interface IChatService
    {
        public Task<int> SendMessage(string message, IFormFile mediaFile, ClaimsPrincipal user);
        public Task<string> SaveMediaFile(IFormFile mediaFile);
    }
}