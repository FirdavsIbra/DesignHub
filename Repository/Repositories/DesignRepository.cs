using AutoMapper;
using database;
using database.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class DesignRepository : IDesignRepository
    {
        private readonly IMapper _mapper;
        private readonly ContextDb _dbContext;

        public DesignRepository(IMapper mapper, ContextDb dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IDesign> GetById(int id)
        {
            var designEntity = await _dbContext.Designs.FindAsync(id);
            return _mapper.Map<IDesign>(designEntity);
        }

        public async Task<IEnumerable<IDesign>> GetAll()
        {
            var designEntities = await _dbContext.Designs.ToListAsync();
            return _mapper.Map<IEnumerable<IDesign>>(designEntities);
        }

        public async Task Add(IDesign design, IFormFile file)
        {
            var designEntity = _mapper.Map<Design>(design);

            // Save the design file to a location of your choice
            string filePath = await SaveDesignFileAsync(file);
            designEntity.ImageUrl = filePath;

            _dbContext.Designs.Add(designEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(IDesign design)
        {
            var designEntity = await _dbContext.Designs.FindAsync(design.Id);
            if (designEntity == null)
                throw new Exception("Design not found");

            _mapper.Map(design, designEntity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var designEntity = await _dbContext.Designs.FindAsync(id);
            if (designEntity == null)
                throw new Exception("Design not found");

            _dbContext.Designs.Remove(designEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<string> SaveDesignFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            // Generate a unique filename for the design file
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Save the design file to a location of your choice (e.g., a designated folder)
            string filePath = Path.Combine("DesignFiles", uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}
