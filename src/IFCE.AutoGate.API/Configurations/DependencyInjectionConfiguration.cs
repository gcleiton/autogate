using FluentValidation;
using IFCE.AutoGate.Application;
using IFCE.AutoGate.Core.Communication;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Infrastructure.Gateways;
using IFCE.AutoGate.Infrastructure.Repositories;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.API.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void ConfigureDependencies(this IServiceCollection services)
    {
        services.AddMediatR(ApplicationAssembly.Value);
        services.AddValidatorsFromAssembly(ApplicationAssembly.Value);

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IAdministratorRepository, AdministratorRepository>();
        services.AddScoped<IMailSender, MailKitMailSender>();
        services.AddScoped<IHasher>(s => new BcryptHasher(12));
        services.AddScoped<ITokenHandler, JwtTokenHandler>();
        services.AddScoped<INotification, Notification>();
    }
}
