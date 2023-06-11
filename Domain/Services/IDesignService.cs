using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.Services
{
    public interface IDesignService
    {
        public Task<int> UploadDesignAsync(IFormFile file);
    }
}
