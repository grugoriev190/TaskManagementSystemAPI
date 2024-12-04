using System.IdentityModel.Tokens.Jwt;
using TaskManagementSystem.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace TaskManagementSystem.Auth
{
	// Сервіс для генерування JWT токенів
	public class JwtService(IOptions<AuthSettings> options)
    {
		// Метод для генерації токена на основі даних користувача
		public string GenerateToken(User account)
        {
            var claims = new List<Claim>
            {
                new Claim("userName", account.UserName),
                new Claim("email",  account.Email),
                new Claim("id", account.Id.ToString()),
            };
            var jwtToken = new JwtSecurityToken(
                                expires: DateTime.UtcNow.Add(options.Value.Expires), // Встановлюємо строк дії токена
								claims: claims,
                                signingCredentials: new SigningCredentials(
                                    new SymmetricSecurityKey(
                                        Encoding.UTF8.GetBytes(options.Value.SecretKey)), // Ключ для підпису токена
											SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
