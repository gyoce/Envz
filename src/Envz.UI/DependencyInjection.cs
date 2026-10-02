using Envz.Common;
using Envz.Common.Services;
using Envz.Common.Services.Dialogs;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Dialogs.Confirm;
using Envz.Common.UI.Dialogs.Error;
using Envz.UI.Services;
using Envz.UI.Views;
using Envz.UI.Views.Pages.Applications.AddApplication;
using Envz.UI.Views.Pages.Applications.HomeApplications;
using Envz.UI.Views.Pages.Environments.CreateEnvironment;
using Envz.UI.Views.Pages.Environments.EditEnvironment;
using Envz.UI.Views.Pages.Environments.HomeEnvironments;
using Envz.UI.Views.Pages.Environments.SelectApplication;
using Envz.UI.Views.Pages.Home;
using Envz.UI.Views.Pages.Settings;
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
        services.AddSingleton<GlobalExceptionHandler>();
        services.AddSingleton<ViewLocator>();

        services.AddSingleton<ApplicationItemViewModelFactory>();
        services.AddSingleton<EnvironmentApplicationItemViewModelFactory>();
        services.AddSingleton<EnvironmentItemViewModelFactory>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddPage<HomePage, HomePageViewModel>();
        services.AddPage<HomeEnvironmentsPage, HomeEnvironmentsPageViewModel>();
        services.AddPage<CreateEnvironmentPage, CreateEnvironmentPageViewModel>();
        services.AddPage<EditEnvironmentPage, EditEnvironmentPageViewModel>();
        services.AddPage<SelectApplicationPage, SelectApplicationPageViewModel>();
        services.AddPage<HomeApplicationsPage, HomeApplicationsPageViewModel>();
        services.AddPage<AddApplicationPage, AddApplicationPageViewModel>();
        services.AddPage<SettingsPage, SettingsPageViewModel>();

        services.AddDialog<ErrorDialog, ErrorDialogViewModel>();
        services.AddDialog<ConfirmDialog, ConfirmDialogViewModel>();

        return services;
    }
}