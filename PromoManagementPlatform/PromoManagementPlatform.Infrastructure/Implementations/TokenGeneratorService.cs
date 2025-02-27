using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PromoManagementPlatform.Application.Abstract;
using PromoManagementPlatform.Domain.Entities;
using PromoManagementPlatform.Infrastructure.Config;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PromoManagementPlatform.Infrastructure.Implementations
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly JwtOptions _jwtOptions;

        public TokenGeneratorService(IOptions<JwtOptions> options)
            => _jwtOptions = options.Value;

        public Task<string> GenerateTokenAsync(ApplicationUser user, List<string> userRoles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.Expires),
                signingCredentials: credentials
            );
            
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
