using Envz.Domain.Ports;
using Envz.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Envz.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IEnvironmentRepository, InMemoryEnvironmentRepository>();
        return services;
    }
}