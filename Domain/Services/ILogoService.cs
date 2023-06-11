
namespace Domain.Services
{
    public interface ILogoService
    {
        Task<bool> UploadLogo(int companyId, byte[] logoData);
    }
}
