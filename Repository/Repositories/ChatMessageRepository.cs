using AutoMapper;
using database;
using database.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Business_Models;

namespace Repository.Repositories
{
    public class ChatMessageRepository : IChatRepository
    {
        private readonly IMapper _mapper;
        private readonly ContextDb _dbContext;

        public ChatMessageRepository(ContextDb dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateAsync(IChatMessage message)
        {
            var chatMessage = _mapper.Map<ChatMessage>(message);

            await _dbContext.ChatMessages.AddAsync(chatMessage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IChatMessage> GetById(int id)
        {
            var chatMessage = await _dbContext.Set<ChatMessage>().FindAsync(id);
            return _mapper.Map<IChatMessage>(chatMessage);
        }

        public async Task<IChatMessage[]> GetAllAsync()
        {
            return await _dbContext.ChatMessages.Select(x => _mapper.Map<ChatMessageBusiness>(x)).ToArrayAsync();
        }

        public async Task<IChatMessage[]> GetAllMessagesByRecieverId(int id)
        {
            return await _dbContext.ChatMessages
                .Where(x => x.RecieverId == id)
                .Select(x => _mapper.Map<ChatMessageBusiness>(x))
                .ToArrayAsync();
        }

        public async Task<IChatMessage[]> GetAllMessagesByRecieverAndSenderId(int recieverId, int senderId)
        {
            return await _dbContext.ChatMessages
                .Where(x => x.RecieverId == recieverId && x.SenderId == senderId)
                .Select(x => _mapper.Map<ChatMessageBusiness>(x))
                .ToArrayAsync();
        }
    }
}
