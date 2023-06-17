﻿using AutoMapper;
using database;
using database.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Business_Models;

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

        public async Task<ICompany> GetByUserIdAsync(int userId)
        {
            var companyEntity = await _dbContext.Companies.FirstOrDefaultAsync(c => c.UserId == userId);
            return _mapper.Map<CompanyBusiness>(companyEntity);
        }

        public async Task<ICompany[]> GetAllAsync()
        {
            return await _dbContext.Companies.Select(x => _mapper.Map<CompanyBusiness>(x)).ToArrayAsync();
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

            var a =_mapper.Map(company, existingCompany);
            _dbContext.Companies.Update(a);
            await _dbContext.SaveChangesAsync();
        }
    }
}
