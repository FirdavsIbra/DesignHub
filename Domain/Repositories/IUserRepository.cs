using Domain.Models;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IUser> GetById(int id);
        Task<IUser> GetByUsername(string username);
        Task Add(IUser user);
        Task Update(IUser user);
        Task Delete(int id);
    }
}
