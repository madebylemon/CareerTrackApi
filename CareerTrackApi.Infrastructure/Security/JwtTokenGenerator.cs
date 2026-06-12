using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CareerTrackApi.Application.Interfaces;
using CareerTrackApi.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CareerTrackApi.Infrastructure.Security
{
    // Lớp hỗ trợ tạo chuỗi JWT Token từ thông tin User
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Tạo JWT Token cho User
        public string GenerateToken(User user)
        {
            var secretKey = _configuration["Jwt:Secret"] ?? "SuperSecretKeyThatIsAtLeast32BytesLong!";
            var issuer = _configuration["Jwt:Issuer"] ?? "CareerTrackApi";
            var audience = _configuration["Jwt:Audience"] ?? "CareerTrackApiUsers";
            var expiryMinutes = double.TryParse(_configuration["Jwt:ExpiryInMinutes"], out var minutes) ? minutes : 120;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
