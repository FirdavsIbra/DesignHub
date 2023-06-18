using AutoMapper;
using database;
using database.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Business_Models;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextDb _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ContextDb dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IUser[]> GetAllAsync()
        {
            return await _dbContext.Users.Where(x => x.Id != 1).Select(x => _mapper.Map<UserBusiness>(x)).ToArrayAsync();
        }

        public async Task<IUser> GetByIdAsync(int id)
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            return _mapper.Map<UserBusiness>(userEntity);
        }

        public async Task<IUser> GetByUsernameAsync(string username)
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (userEntity == null)
            {
                return null; // No user found with the specified username
            }
            return _mapper.Map<UserBusiness>(userEntity);
        }

        public async Task Add(IUser user)
        {
            var userEntity = _mapper.Map<User>(user);
            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(IUser user)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser == null)
                throw new InvalidOperationException("User not found");

            _mapper.Map(user, existingUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userEntity == null)
                throw new InvalidOperationException("User not found");

            _dbContext.Users.Remove(userEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetUserNameByIdAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user.Username;
        }

        public async Task<int> GetUserIdByUsername(string username)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user?.Id ?? 0;
        }
    }
}
