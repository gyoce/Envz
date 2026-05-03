using EnvBooster.UI.ViewModels;
using EnvBooster.UI.ViewModels.Pages;
using EnvBooster.UI.ViewModels.Pages.Environments;
using EnvBooster.UI.ViewModels.Pages.Environments.SubPages;
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

        services.AddSingleton<EnvironmentsPageViewModel>();
        services.AddSingleton<HomeEnvironmentsSubPageViewModel>();
        services.AddSingleton<EditEnvironmentSubPageViewModel>();
        services.AddSingleton<EnvironmentViewModelFactory>();
        services.AddTransient<EnvironmentViewModel>();

        services.AddSingleton<SettingsPageViewModel>();

        return services;
    }
}

public class EnvironmentViewModelFactory(IServiceProvider serviceProvider)
{
    public EnvironmentViewModel Create(Environment environment)
        => ActivatorUtilities.CreateInstance<EnvironmentViewModel>(serviceProvider, environment);
}