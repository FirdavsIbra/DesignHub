
namespace Domain.Services
{
    public interface ILogoService
    {
        public Task<bool> UploadLogoAsync(int companyId, byte[] logoData);
    }
}
