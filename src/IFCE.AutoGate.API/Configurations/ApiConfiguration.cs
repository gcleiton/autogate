using Microsoft.AspNetCore.Mvc;

namespace IFCE.AutoGate.API.Configurations;

public static class ApiConfiguration
{
    public static void AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddDateOnlyTimeOnlyStringConverters();
        services.AddEndpointsApiExplorer();
        services.AddCors(o =>
        {
            o.AddPolicy("default", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseCors("default");
    }
}
