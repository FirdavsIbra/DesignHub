using AutoMapper;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly JwtModel _jwtOptions;

        public AuthService(IConfiguration configuration, IUserRepository userRepository, IOptions<JwtModel> jwtOptions)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateJwtToken(string username)
        {
            var secretKey = Encoding.UTF8.GetBytes(_jwtOptions.SecretKey);

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, username)
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidateJwtToken(string token)
        {
            try
            {
                // Получение секретного ключа из конфигурации
                var secretKey = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task RegisterUser(string username, string password)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user != null)
            {
                throw new Exception("This username is already taken.");
            }
            var newUser = new UserDTO
            {
                Username = username,
                Password = HashPassword(password) // Хеширование пароля
            };

           await _userRepository.Add(newUser);
        }

        public async Task<bool> Login(string username, string password)
        {
            // Получение пользователя по имени пользователя
            var user = await _userRepository.GetByUsername(username);
            if (user == null)
            {
                return false; // Пользователь не найден
            }

            // Проверка пароля
            if (!VerifyPassword(password, user.Password))
            {
                return false; // Неправильный пароль
            }

            return true; // Аутентификация успешна
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashBytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            string inputHash = HashPassword(password);
            return hashedPassword == inputHash;
        }

        public async Task<int> GetCurrentUserId(ClaimsPrincipal user)
        {
            string userIdString = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdString, out int userId))
            {
                // Проверка наличия пользователя в базе данных по его идентификатору
                var existingUser = await _userRepository.GetById(userId);
                return existingUser != null ? userId : 0;
            }

            // Если идентификатор пользователя не может быть преобразован в int,
            // считаем пользователя незарегистрированным
            return 0;
        }
    }
}
