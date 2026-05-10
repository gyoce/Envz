using Envz.UI.Services.Navigation;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Environments;

public class EnvironmentsPageViewModel : ViewModelWithMenu
{
    public EnvironmentsPageViewModel([FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService) : base(navigationService)
    {
        NavigationService.NavigateTo<HomeEnvironmentsSubPageViewModel>();
    }
}