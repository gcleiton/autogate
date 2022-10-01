namespace IFCE.AutoGate.API.Configurations;

public static class BuilderConfiguration
{
    public static IConfiguration SetDefaultConfiguration(this ConfigurationManager configuration,
        IWebHostEnvironment environment)
    {
        var builder = configuration
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
