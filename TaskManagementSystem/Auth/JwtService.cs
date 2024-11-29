using System.IdentityModel.Tokens.Jwt;
using TaskManagementSystem.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace TaskManagementSystem.Auth
{
    public class JwtService(IOptions<AuthSettings> options)
    {
        public string GenerateToken(User account)
        {
            var claims = new List<Claim>
            {
                new Claim("userName", account.UserName),
                new Claim("email",  account.Email),
                new Claim("id", account.Id.ToString()),
            };
            var jwtToken = new JwtSecurityToken(
                                expires: DateTime.UtcNow.Add(options.Value.Expires),
                                claims: claims,
                                signingCredentials: new SigningCredentials(
                                    new SymmetricSecurityKey(
                                        Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                                            SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
