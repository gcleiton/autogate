using Microsoft.AspNetCore.Mvc;

namespace IFCE.AutoGate.API.Configurations;

public static class ApiConfiguration
{
    public static void AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.MapControllers();
    }
}
