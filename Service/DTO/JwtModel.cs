
namespace Service.DTO
{
    public class JwtOptions
    {
        public string? SecretKey { get; set; }
        public int ExpirationDays { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
