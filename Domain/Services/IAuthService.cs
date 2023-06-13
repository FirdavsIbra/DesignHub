﻿using System.Security.Claims;

namespace Domain.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string username, int userId);
        bool ValidateJwtToken(string token);
        public Task RegisterUser(string username, string password);
        public Task<bool> Login(string username, string password);
        public Task<int> GetCurrentUserId(ClaimsPrincipal user);
        public Task<int> GetUserByUserNameAsync(string username);
    }

}
