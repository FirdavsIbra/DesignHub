using Domain.Models;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<IUser[]> GetAllAsync();
        Task<IUser> GetById(int id);
        Task<IUser> GetByUsername(string username);
        Task Add(IUser user);
        Task Update(IUser user);
        Task Delete(int id);
        Task<int> GetUserIdByUsername(string username);
        Task<string> GetUserNameByIdAsync(int id);
    }
}
