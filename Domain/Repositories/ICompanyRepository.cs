using Domain.Models;

namespace Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<ICompany[]> GetAllAsync();
        Task<ICompany> GetByUserIdAsync(int id);
        Task Add(ICompany company);
        Task Update(ICompany company);
    }
}
