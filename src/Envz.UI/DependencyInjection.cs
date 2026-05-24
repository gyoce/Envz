using Envz.UI.Services;
using Envz.UI.Services.Dialogs;
using Envz.UI.Services.Navigation;
using Envz.UI.ViewModels;
using Envz.UI.ViewModels.Dialogs;
using Envz.UI.ViewModels.Pages.Applications;
using Envz.UI.ViewModels.Pages.Environments;
using Envz.UI.ViewModels.Pages.Home;
using Envz.UI.ViewModels.Pages.Settings;
using Envz.UI.ViewModels.UserControls;
using Envz.UI.Views;
using Envz.UI.Views.Dialogs;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddUi(this IServiceCollection services)
    {
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IFileDialogService, FileDialogService>();
        services.AddSingleton<IIconExtractor, IconExtractor>();

        services.AddSingleton<EnvironmentViewModelFactory>();
        services.AddSingleton<ApplicationViewModelFactory>();
        services.AddSingleton<EnvironmentApplicationViewModelFactory>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddTransient<SelectApplicationDialog>();
        services.AddTransient<SelectApplicationDialogViewModel>();

        services.AddSingleton<HomePageViewModel>();

        services.AddSingleton<EnvironmentsPageViewModel>();
        services.AddSingleton<HomeEnvironmentsPageViewModel>();
        services.AddSingleton<CreateEnvironmentPageViewModel>();
        services.AddSingleton<EditEnvironmentPageViewModel>();
        services.AddTransient<EnvironmentViewModel>();

        services.AddSingleton<ApplicationsPageViewModel>();
        services.AddSingleton<HomeApplicationsPageViewModel>();
        services.AddSingleton<AddApplicationPageViewModel>();

        services.AddSingleton<SettingsPageViewModel>();

        return services;
    }
}