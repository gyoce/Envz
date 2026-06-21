using Envz.Common.Services.Navigation;
using Envz.UI.Services;
using Envz.UI.Services.Dialogs;
using Envz.UI.Views;
using Envz.UI.Views.Pages.Applications;
using Envz.UI.Views.Pages.Applications.AddApplication;
using Envz.UI.Views.Pages.Applications.HomeApplications;
using Envz.UI.Views.Pages.Environments;
using Envz.UI.Views.Pages.Environments.CreateEnvironment;
using Envz.UI.Views.Pages.Environments.EditEnvironment;
using Envz.UI.Views.Pages.Environments.HomeEnvironments;
using Envz.UI.Views.Pages.Environments.SelectApplication;
using Envz.UI.Views.Pages.Home;
using Envz.UI.Views.Pages.Settings;
using Envz.UI.Views.UserControls.EnvironmentItem;

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
        services.AddSingleton<ViewModelFactory>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddSingleton<HomePageViewModel>();

        services.AddSingleton<HomeEnvironmentsPageViewModel>();
        services.AddSingleton<CreateEnvironmentPageViewModel>();
        services.AddSingleton<EditEnvironmentPageViewModel>();
        services.AddSingleton<SelectApplicationPageViewModel>();
        services.AddTransient<EnvironmentItemViewModel>();
        
        services.AddSingleton<HomeApplicationsPageViewModel>();
        services.AddSingleton<AddApplicationPageViewModel>();

        services.AddSingleton<SettingsPageViewModel>();

        return services;
    }
}