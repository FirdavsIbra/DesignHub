using Domain.Models;
using Domain.Repositories;
using Domain.Services;

namespace Service.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IUser[]> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<string> GetUsernameAsync(int id)
        {
            return await _userRepository.GetUserNameByIdAsync(id);
        }
    }
}
