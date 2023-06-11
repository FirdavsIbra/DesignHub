using AutoMapper;
using database;
using database.Entities;
using Domain.Models;

namespace Repository.Repositories
{
    public class FileRepository
    {
        private readonly ContextDb _dbContext;
        private readonly IMapper _mapper;

        public FileRepository(ContextDb dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Add(IFileEntity file)
        {
            var fileEntity = _mapper.Map<FileEntity>(file);
            _dbContext.FileEntities.Add(fileEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
