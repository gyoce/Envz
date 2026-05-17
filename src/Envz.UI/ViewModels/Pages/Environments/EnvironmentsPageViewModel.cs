using System.Windows.Input;

using Envz.UI.Services.Navigation;
using Envz.UI.Utils;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Environments;

public class EnvironmentsPageViewModel : ViewModelWithMenu
{
    public ICommand NavigateToHomeCommand { get; }

    public EnvironmentsPageViewModel([FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService) : base(navigationService)
    {
        NavigateToHomeCommand = new RelayCommand(_ => NavigationService.NavigateTo<HomeEnvironmentsSubPageViewModel>());
        NavigationService.NavigateTo<HomeEnvironmentsSubPageViewModel>();
    }
}