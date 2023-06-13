
namespace Service.DTO
{
    public class JwtModel
    {
        public string SecretKey { get; set; }
        public int ExpirationDays { get; set; }
    }
}
