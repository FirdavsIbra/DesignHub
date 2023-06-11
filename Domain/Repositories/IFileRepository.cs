using Domain.Models;

namespace Domain.Repositories
{
    public interface IFileRepository
    {
        public Task Add(IFileEntity file);
    }
}
