using Domain.Models;

namespace Domain.Services
{
    public interface ICompanyService
    {
        public Task AddCompany(ICompany company);
    }
}
