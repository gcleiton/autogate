using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IFCE.AutoGate.Core.Settings;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IFCE.AutoGate.Infrastructure.Gateways;

public class JwtTokenHandler : ITokenHandler
{
    private readonly JWTSettings _jwtSettings;

    public JwtTokenHandler(IOptions<JWTSettings> settings)
    {
        _jwtSettings = settings.Value;
    }

    public async Task<string> Generate(Administrator administrator)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.Key);

        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim("administratorId", administrator.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, administrator.Name));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, administrator.Email));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddMilliseconds(_jwtSettings.ExpirationInMs),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
