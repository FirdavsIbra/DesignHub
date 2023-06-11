using System.Security.Claims;

namespace Domain.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string username);
        bool ValidateJwtToken(string token);
        public Task<bool> RegisterUser(string username, string password);
        public Task<bool> Login(string username, string password);
        public int GetCurrentUserId(ClaimsPrincipal user);
    }

}
