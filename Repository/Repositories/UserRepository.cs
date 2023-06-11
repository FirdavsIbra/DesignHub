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

        public async Task<IUser> GetById(int id)
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<IUser>(userEntity);
        }

        public async Task<IUser> GetByUsername(string username)
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
            _dbContext.Users.Add(userEntity);
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
    }
}
