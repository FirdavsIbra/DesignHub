using Domain.Models;

namespace Domain.Services
{
    public interface IUserService
    {
        public Task<IUser[]> GetAllAsync();
        public Task<string> GetUsernameAsync(int id);
    }
}
