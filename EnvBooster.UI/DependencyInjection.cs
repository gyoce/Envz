using EnvBooster.UI.ViewModels;
using EnvBooster.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddUi(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<HomePageViewModel>();
        services.AddSingleton<SettingsPageViewModel>();
        services.AddSingleton<CreateEnvironmentPageViewModel>();
        return services;
    }
}
