using Envz.UI.Services.Navigation;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Applications;

public class ApplicationsPageViewModel : ViewModelWithMenu
{
    public ApplicationsPageViewModel([FromKeyedServices(ENavigationRegion.Applications)] INavigationService navigationService) : base(navigationService)
    {
        NavigationService.NavigateTo<HomeApplicationsSubPageViewModel>();
    }
}