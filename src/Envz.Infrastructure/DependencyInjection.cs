using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration;
using Envz.Infrastructure.Configuration.Stores;
using Envz.Infrastructure.Persistence;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<IConfigurationFilesPathProvider, ConfigurationFilesPathProvider>();
        services.AddSingleton<IConfigurationStore, ConfigurationStore>();
        services.AddSingleton<IIconStore, IconStore>();

        services.AddSingleton<IEnvironmentRepository, EnvironmentRepository>();
        services.AddSingleton<IApplicationRepository, ApplicationRepository>();

        return services;
    }
}