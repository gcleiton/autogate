using IFCE.AutoGate.Core.Settings;

namespace IFCE.AutoGate.API.Configurations;

public static class SettingsConfiguration
{
    public static void AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
    }
}
