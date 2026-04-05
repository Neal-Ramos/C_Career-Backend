using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.commons.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.TokenServices
{
    public class TokenServices: ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(
            Guid AdminId,
            string Role
        )
        {
            var secretKey = _configuration["JwtSettings:Secret"]?? 
                throw new InvalidOperationException("JWT secret key is missing in configuration.");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, AdminId.ToString()),
                new Claim(ClaimTypes.Role, Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}