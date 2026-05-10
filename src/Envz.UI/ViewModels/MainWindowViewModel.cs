using System.Windows.Input;

using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.Pages;
using Envz.UI.ViewModels.Pages.Applications;
using Envz.UI.ViewModels.Pages.Environments;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels;

public class MainWindowViewModel : ViewModelWithMenu
{
    public ICommand ShowHomePageCommand { get; }
    public ICommand ShowEnvironmentsPageCommand { get; }
    public ICommand ShowSettingsPageCommand { get; }
    public ICommand ShowApplicationsPageCommand { get; }

    public MainWindowViewModel([FromKeyedServices(ENavigationRegion.Main)] INavigationService navigationService) : base(navigationService)
    {
        ShowHomePageCommand = new RelayCommand(_ => NavigationService.NavigateTo<HomePageViewModel>());
        ShowEnvironmentsPageCommand = new RelayCommand(_ => NavigationService.NavigateTo<EnvironmentsPageViewModel>());
        ShowApplicationsPageCommand = new RelayCommand(_ => NavigationService.NavigateTo<ApplicationsPageViewModel>());
        ShowSettingsPageCommand = new RelayCommand(_ => NavigationService.NavigateTo<SettingsPageViewModel>());

        NavigationService.NavigateTo<HomePageViewModel>();
    }
}
