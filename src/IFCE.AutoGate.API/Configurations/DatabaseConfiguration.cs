using IFCE.AutoGate.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IFCE.AutoGate.API.Configurations;

public static class DatabaseConfiguration
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}
