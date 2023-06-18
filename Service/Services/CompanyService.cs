using Domain.Models;
using Domain.Repositories;
using Domain.Services;

namespace Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// Add company if the user has not created company yet, if he has company, then update this.
        /// </summary>
        public async Task AddOrUpdateCompanyAsync(ICompany company)
        {
            var companies = await _companyRepository.GetAllAsync();
            var existingCompany = companies.FirstOrDefault(x => x.UserId == company.UserId);
            if (existingCompany != null)
            {
                company.Id = existingCompany.Id;
                await _companyRepository.UpdateAsync(company);
            }
            else
            {
                await _companyRepository.AddAsync(company);
            }
        }

        public async Task<ICompany> GetByUserIdAsync(int userId)
        {
           return await _companyRepository.GetByUserIdAsync(userId);
        }
    }
}
