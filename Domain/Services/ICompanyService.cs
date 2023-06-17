using Domain.Models;

namespace Domain.Services
{
    public interface ICompanyService
    {
        public Task AddOrUpdateCompanyAsync(ICompany company);

        public Task<ICompany> GetByUserIdAsync(int userId);
    }
}
