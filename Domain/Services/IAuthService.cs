using System.Security.Claims;

namespace Domain.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string username, int userId);
        public bool ValidateJwtToken(string token);
        public Task RegisterUserAsync(string username, string password);
        public Task<bool> LoginAsync(string username, string password);
        public Task<int> GetCurrentUserIdAsync(ClaimsPrincipal user);
        public Task<int> GetUserByUserNameAsync(string username);
    }

}
