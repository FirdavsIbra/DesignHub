using Domain.Models;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<IUser[]> GetAllAsync();
        Task<IUser> GetByIdAsync(int id);
        Task<IUser> GetByUsernameAsync(string username);
        Task Add(IUser user);
        Task Update(IUser user);
        Task Delete(int id);
        Task<int> GetUserIdByUsername(string username);
        Task<string> GetUserNameByIdAsync(int id);
    }
}
