using AutoMapper;
using database;
using database.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ChatMessageRepository : IChatRepository
    {
        private readonly IMapper _mapper;
        private readonly ContextDb _dbContext;

        public ChatMessageRepository(IMapper mapper, ContextDb dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IChatMessage> GetById(int id)
        {
            var chatMessageEntity = await _dbContext.ChatMessages.FindAsync(id);
            return _mapper.Map<IChatMessage>(chatMessageEntity);
        }

        public async Task<IEnumerable<IChatMessage>> GetAll()
        {
            var chatMessageEntities = await _dbContext.ChatMessages.ToListAsync();
            return _mapper.Map<IEnumerable<IChatMessage>>(chatMessageEntities);
        }

        public async Task Add(IChatMessage chatMessage)
        {
            var chatMessageEntity = _mapper.Map<ChatMessage>(chatMessage);
            _dbContext.ChatMessages.Add(chatMessageEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(IChatMessage chatMessage)
        {
            var chatMessageEntity = await _dbContext.ChatMessages.FindAsync(chatMessage.Id);
            if (chatMessageEntity == null)
                throw new Exception("Chat message not found");

            _mapper.Map(chatMessage, chatMessageEntity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var chatMessageEntity = await _dbContext.ChatMessages.FindAsync(id);
            if (chatMessageEntity == null)
                throw new Exception("Chat message not found");

            _dbContext.ChatMessages.Remove(chatMessageEntity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<IChatMessage>> GetMessagesByUserId(int userId)
        {
            var chatMessageEntities = await _dbContext.ChatMessages
                .Where(c => c.SenderId == userId || c.RecieverId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<IChatMessage>>(chatMessageEntities);
        }
    }
}
