using AutoMapper;
using database;
using database.Entities;
using Domain.Models;
using Domain.Repositories;

namespace Repository.Repositories
{
    public class ChatMessageRepository : IChatRepository
    {
        private readonly IMapper _mapper;
        private readonly ContextDb _dbContext;

        public ChatMessageRepository(ContextDb dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Create(IChatMessage message)
        {
            var chatMessage = _mapper.Map<ChatMessage>(message);
            await _dbContext.Set<ChatMessage>().AddAsync(chatMessage);
            await _dbContext.SaveChangesAsync();
            return chatMessage.Id;
        }

        public async Task<IChatMessage> GetById(int id)
        {
            var chatMessage = await _dbContext.Set<ChatMessage>().FindAsync(id);
            return _mapper.Map<IChatMessage>(chatMessage);
        }
    }
}
