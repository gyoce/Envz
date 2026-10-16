using Envz.Common;
using Envz.Common.Services;
using Envz.Common.Services.Dialogs;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Dialogs.Confirm;
using Envz.Common.UI.Dialogs.Error;
using Envz.Domain.Entities;
using Envz.UI.Services;
using Envz.UI.Views;
using Envz.UI.Views.Pages.Applications.AddApplication;
using Envz.UI.Views.Pages.Applications.AddCustomApplication;
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
    extension(IServiceCollection services)
    {
        public IServiceCollection AddUi()
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
            services.AddSingleton<SelectableApplicationItemViewModelFactory>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddPages();
            services.AddDialogs();
            services.AddBasicSuggestedApplications();

            return services;
        }

        private IServiceCollection AddDialogs()
        {
            services.AddDialog<ErrorDialog, ErrorDialogViewModel>();
            services.AddDialog<ConfirmDialog, ConfirmDialogViewModel>();

            return services;
        }

        private IServiceCollection AddPages()
        {
            services.AddPage<HomePage, HomePageViewModel>();

            services.AddPage<HomeEnvironmentsPage, HomeEnvironmentsPageViewModel>();
            services.AddPage<CreateEnvironmentPage, CreateEnvironmentPageViewModel>();
            services.AddPage<EditEnvironmentPage, EditEnvironmentPageViewModel>();

            services.AddPage<SelectApplicationPage, SelectApplicationPageViewModel>();
            services.AddPage<HomeApplicationsPage, HomeApplicationsPageViewModel>();
            services.AddPage<AddApplicationPage, AddApplicationPageViewModel>();
            services.AddPage<AddCustomApplicationPage, AddCustomApplicationPageViewModel>();

            services.AddPage<SettingsPage, SettingsPageViewModel>();

            return services;
        }

        private IServiceCollection AddBasicSuggestedApplications()
        {
            services.AddSuggestedApplications(
                new SuggestedApplicationCandidate("Visual Studio Code", @"%LOCALAPPDATA%\Programs\Microsoft VS Code\Code.exe"),
                new SuggestedApplicationCandidate("Visual Studio 2026", @"%ProgramFiles%\Microsoft Visual Studio\18\Community\Common7\IDE\devenv.exe"),
                new SuggestedApplicationCandidate("Visual Studio 2022", @"%ProgramFiles%\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe"),
                new SuggestedApplicationCandidate("Google Chrome", @"%ProgramFiles%\Google\Chrome\Application\chrome.exe"),
                new SuggestedApplicationCandidate("Firefox", @"%ProgramFiles%\Mozilla Firefox\firefox.exe")
            );
            return services;
        }
    }
}