using EnvBooster.UI.Services;
using EnvBooster.UI.Services.Dialogs;
using EnvBooster.UI.Services.Navigation;
using EnvBooster.UI.ViewModels;
using EnvBooster.UI.ViewModels.Dialogs;
using EnvBooster.UI.ViewModels.Pages;
using EnvBooster.UI.ViewModels.Pages.Environments;
using EnvBooster.UI.ViewModels.UserControls;
using EnvBooster.UI.Views;
using EnvBooster.UI.Views.Dialogs;

using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddUi(this IServiceCollection services)
    {
        services.AddKeyedSingleton<INavigationService, NavigationService>(ENavigationRegion.Main);
        services.AddKeyedSingleton<INavigationService, NavigationService>(ENavigationRegion.Environments);
        services.AddSingleton<IDialogService, DialogService>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddSingleton<SelectApplicationDialog>();
        services.AddSingleton<SelectApplicationDialogViewModel>();

        services.AddSingleton<HomePageViewModel>();

        services.AddSingleton<EnvironmentsPageViewModel>();
        services.AddSingleton<HomeEnvironmentsSubPageViewModel>();
        services.AddSingleton<CreateEnvironmentSubPageViewModel>();
        services.AddSingleton<EditEnvironmentSubPageViewModel>();
        services.AddSingleton<EnvironmentViewModelFactory>();
        services.AddTransient<EnvironmentViewModel>();

        services.AddSingleton<SettingsPageViewModel>();

        return services;
    }
}