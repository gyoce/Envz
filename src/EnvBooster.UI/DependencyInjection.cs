using EnvBooster.UI.ViewModels.Pages;
using EnvBooster.UI.ViewModels.UserControls;
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
        services.AddSingleton<EnvironmentViewModelFactory>();
        services.AddTransient<EnvironmentViewModel>();
        return services;
    }
}

public class EnvironmentViewModelFactory(IServiceProvider serviceProvider)
{
    public EnvironmentViewModel Create(Environment environment)
        => ActivatorUtilities.CreateInstance<EnvironmentViewModel>(serviceProvider, environment);
}