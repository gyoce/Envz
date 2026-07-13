using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration;
using Envz.Infrastructure.Persistence;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<IConfigurationPathProvider, ConfigurationPathProvider>();
        services.AddSingleton<IConfigurationStore, ConfigurationStore>();

        services.AddSingleton<IEnvironmentRepository, EnvironmentRepository>();
        services.AddSingleton<IApplicationRepository, ApplicationRepository>();
        return services;
    }
}