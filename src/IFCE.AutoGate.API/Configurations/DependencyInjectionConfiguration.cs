using FluentValidation;
using IFCE.AutoGate.Application;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Infrastructure.Gateways;
using IFCE.AutoGate.Infrastructure.Repositories;
using MediatR;

namespace IFCE.AutoGate.API.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void ConfigureDependencies(this IServiceCollection services)
    {
        services.AddMediatR(ApplicationAssembly.Value);
        services.AddValidatorsFromAssembly(ApplicationAssembly.Value);

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IAdministratorRepository, AdministratorRepository>();
    }
}
