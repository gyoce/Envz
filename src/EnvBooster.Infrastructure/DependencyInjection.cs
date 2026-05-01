using EnvBooster.Domain.Ports;
using EnvBooster.Infrastructure.Persistence;

using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IEnvironmentRepository, InMemoryEnvironmentRepository>();
        return services;
    }
}