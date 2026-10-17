using Envz.Common;
using Envz.Common.Services;
using Envz.Common.Services.Dialogs;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Dialogs.Confirm;
using Envz.Common.UI.Dialogs.Error;
using Envz.Domain.Entities;
using Envz.UI.Services;
using Envz.UI.Views;
using Envz.UI.Views.Pages.Home;
using Envz.UI.Views.Pages.Settings;
using Envz.UI.Views.Pages.Tools.AddCustomTool;
using Envz.UI.Views.Pages.Tools.AddTool;
using Envz.UI.Views.Pages.Tools.HomeTools;
using Envz.UI.Views.Pages.Workspaces.CreateWorkspace;
using Envz.UI.Views.Pages.Workspaces.EditWorkspace;
using Envz.UI.Views.Pages.Workspaces.HomeWorkspaces;
using Envz.UI.Views.Pages.Workspaces.SelectTool;
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

            services.AddSingleton<ToolItemViewModelFactory>();
            services.AddSingleton<WorkspaceToolItemViewModelFactory>();
            services.AddSingleton<WorkspaceItemViewModelFactory>();
            services.AddSingleton<SelectableToolItemViewModelFactory>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddPages();
            services.AddDialogs();
            services.AddBasicSuggestedTools();

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

            services.AddPage<HomeWorkspacesPage, HomeWorkspacesPageViewModel>();
            services.AddPage<CreateWorkspacePage, CreateWorkspacePageViewModel>();
            services.AddPage<EditWorkspacePage, EditWorkspacePageViewModel>();

            services.AddPage<SelectToolPage, SelectToolPageViewModel>();
            services.AddPage<HomeToolsPage, HomeToolsPageViewModel>();
            services.AddPage<AddToolPage, AddToolPageViewModel>();
            services.AddPage<AddCustomToolPage, AddCustomToolPageViewModel>();

            services.AddPage<SettingsPage, SettingsPageViewModel>();

            return services;
        }

        private IServiceCollection AddBasicSuggestedTools()
        {
            services.AddSuggestedTools(
                new SuggestedToolCandidate("Visual Studio Code", @"%LOCALAPPDATA%\Programs\Microsoft VS Code\Code.exe"),
                new SuggestedToolCandidate("Visual Studio 2026", @"%ProgramFiles%\Microsoft Visual Studio\18\Community\Common7\IDE\devenv.exe"),
                new SuggestedToolCandidate("Visual Studio 2022", @"%ProgramFiles%\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe"),
                new SuggestedToolCandidate("Firefox", @"%ProgramFiles%\Mozilla Firefox\firefox.exe")
            );
            return services;
        }
    }
}