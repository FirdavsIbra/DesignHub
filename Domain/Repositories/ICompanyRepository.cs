using Domain.Models;

namespace Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<ICompany[]> GetAllAsync();
        Task<ICompany> GetByUserIdAsync(int id);
        Task AddAsync(ICompany company);
        Task UpdateAsync(ICompany company);
    }
}
