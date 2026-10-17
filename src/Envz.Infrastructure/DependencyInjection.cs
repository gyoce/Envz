using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration;
using Envz.Infrastructure.Configuration.Stores;
using Envz.Infrastructure.Persistence;
using Envz.Infrastructure.Services;
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

        services.AddSingleton<IWorkspaceRepository, WorkspaceRepository>();
        services.AddSingleton<IToolRepository, ToolRepository>();
        services.AddSingleton<ISettingsRepository, SettingsRepository>();
        services.AddSingleton<IInstalledToolProvider, InstalledToolProvider>();

        return services;
    }
}