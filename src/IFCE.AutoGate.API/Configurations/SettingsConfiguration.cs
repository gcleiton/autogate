using IFCE.AutoGate.Core.Settings;

namespace IFCE.AutoGate.API.Configurations;

public static class SettingsConfiguration
{
    public static void AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
        services.Configure<JWTSettings>(configuration.GetSection(nameof(JWTSettings)));
        services.Configure<AwsS3Settings>(configuration.GetSection(nameof(AwsS3Settings)));
    }
}
