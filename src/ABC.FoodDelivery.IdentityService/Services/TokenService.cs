using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ABC.FoodDelivery.IdentityService.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ABC.FoodDelivery.IdentityService.Services
{

    public class TokenService(IConfiguration config) : ITokenService
    {
        private readonly IConfiguration _config = config;

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("role", user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtSettings:Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JwtSettings:Issuer"),
                audience: Environment.GetEnvironmentVariable("JwtSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}