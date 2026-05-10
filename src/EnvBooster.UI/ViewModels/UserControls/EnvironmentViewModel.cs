using System.Windows.Input;

using EnvBooster.UI.Services.Navigation;
using EnvBooster.UI.Utils;
using EnvBooster.UI.ViewModels.Pages.Environments;

using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI.ViewModels.UserControls;

public class EnvironmentViewModel : ViewModelBase
{
    public ICommand EditEnvironmentCommand { get; }
    public Environment Environment { get; }

    public EnvironmentViewModel([FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService, Environment environment)
    {
        Environment = environment;
        EditEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<EditEnvironmentSubPageViewModel>());
    }
}