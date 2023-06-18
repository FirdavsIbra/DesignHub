using AutoMapper;
using database;
using database.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Business_Models;

namespace Repository.Repositories
{
    public class SketchRepository: ISketchRepository
    {
        private readonly IMapper _mapper;
        private readonly ContextDb _dbContext;
        public SketchRepository(IMapper mapper, ContextDb dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ISketch> GetByUserIdAsync(int userId)
        {
            var sketch = await _dbContext.Sketches.FirstOrDefaultAsync(x => x.UserId == userId);

            return _mapper.Map<SketchBusiness>(sketch);
        }

        public async Task AddAsync(ISketch sketch)
        {
            var sketchEntity = _mapper.Map<Sketch>(sketch);

            await _dbContext.Sketches.AddAsync(sketchEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ISketch[]> GetAllAsync()
        {
            return await _dbContext.Sketches.Select(x => _mapper.Map<SketchBusiness>(x)).ToArrayAsync();
        }

        public async Task UpdateAsync(ISketch sketch)
        {
            var existingSketch = await _dbContext.Sketches.FirstOrDefaultAsync(c => c.Id == sketch.Id);
            if (existingSketch == null)
                throw new InvalidOperationException("Company not found");

            var mappedSketch = _mapper.Map(sketch, existingSketch);
            _dbContext.Sketches.Update(mappedSketch);
            await _dbContext.SaveChangesAsync();
        }

    }
}
