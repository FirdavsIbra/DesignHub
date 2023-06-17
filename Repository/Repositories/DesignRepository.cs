using AutoMapper;
using database;
using database.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Business_Models;

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

        public async Task<IDesign> GetByUserId(int userId)
        {
            var designEntity = await _dbContext.Designs.FirstOrDefaultAsync(x=> x.UserId == userId);
            return _mapper.Map<DesignBusiness>(designEntity);
        }

        public async Task<IDesign[]> GetAllAsync()
        {
            return await _dbContext.Designs.Select(x => _mapper.Map<DesignBusiness>(x)).ToArrayAsync();
        }

        public async Task Add(IDesign design)
        {
            var designEntity = _mapper.Map<Design>(design);

            _dbContext.Designs.Add(designEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(IDesign design)
        {
            var designEntity = await _dbContext.Designs.FirstOrDefaultAsync(d => d.Id == design.Id);
            if (designEntity == null)
                throw new Exception("Design not found");

            var mappedDesign = _mapper.Map(design, designEntity);
            _dbContext.Designs.Update(mappedDesign);
            await _dbContext.SaveChangesAsync();
        }
    }
}
