using Domain.Models;

namespace Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<ICompany> GetById(int id);
        Task Add(ICompany company);
        Task Update(ICompany company);
        Task Delete(int id);
    }
}
