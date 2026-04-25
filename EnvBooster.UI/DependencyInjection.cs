using EnvBooster.UI.ViewModels;
using EnvBooster.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddUi(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<HomePageViewModel>();
        services.AddTransient<SettingsPageViewModel>();
        return services;
    }
}
