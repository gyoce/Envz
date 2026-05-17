using System.Windows.Input;

using Envz.UI.Services.Navigation;
using Envz.UI.Utils;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Applications;

public class ApplicationsPageViewModel : ViewModelWithMenu
{
    public ICommand NavigateToHomeCommand { get; }

    public ApplicationsPageViewModel([FromKeyedServices(ENavigationRegion.Applications)] INavigationService navigationService) : base(navigationService)
    {
        NavigateToHomeCommand = new RelayCommand(_ => NavigationService.NavigateTo<HomeApplicationsSubPageViewModel>());
        NavigationService.NavigateTo<HomeApplicationsSubPageViewModel>();
    }
}