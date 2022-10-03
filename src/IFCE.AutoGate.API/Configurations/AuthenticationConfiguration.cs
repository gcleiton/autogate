using System.Text;
using IFCE.AutoGate.Core.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace IFCE.AutoGate.API.Configurations;

public static class AuthenticationConfiguration
{
    public static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection(nameof(JWTSettings));
        var jwtSettings = jwtSection.Get<JWTSettings>();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        });
        services.AddAuthorization();
    }
}
