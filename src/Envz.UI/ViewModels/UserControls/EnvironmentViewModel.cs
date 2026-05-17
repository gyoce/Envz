using System.Windows.Input;

using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.Pages.Environments;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.UserControls;

public class EnvironmentViewModel : ViewModelBase
{
    public ICommand EditEnvironmentCommand { get; }
    public Environment Environment { get; }
    public int NumberOfApplications => Environment.Applications.Count;

    public EnvironmentViewModel([FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService, Environment environment)
    {
        Environment = environment;
        EditEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<EditEnvironmentSubPageViewModel>());
    }
}