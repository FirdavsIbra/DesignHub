using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;

namespace Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task AddCompany(ICompany company)
        {
            var companyEntity = _mapper.Map<ICompany>(company);
            await _companyRepository.Add(companyEntity);
        }
    }
}
