using AutoMapper;
using database;
using database.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ContextDb _dbContext;
        private readonly IMapper _mapper;

        public CompanyRepository(ContextDb dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ICompany> GetById(int id)
        {
            var companyEntity = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<ICompany>(companyEntity);
        }

        public async Task Add(ICompany company)
        {
            var companyEntity = _mapper.Map<Company>(company);
            _dbContext.Companies.Add(companyEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(ICompany company)
        {
            var existingCompany = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == company.Id);
            if (existingCompany == null)
                throw new InvalidOperationException("Company not found");

            _mapper.Map(company, existingCompany);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var companyEntity = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (companyEntity == null)
                throw new InvalidOperationException("Company not found");

            _dbContext.Companies.Remove(companyEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
