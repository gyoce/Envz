using System.Windows.Input;

using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.Pages.Environments;

namespace Envz.UI.ViewModels.UserControls;

public class EnvironmentViewModel : ViewModelBase
{
    public ICommand EditEnvironmentCommand { get; }
    public Environment Environment { get; }
    public int NumberOfApplications => Environment.Applications.Count;

    public EnvironmentViewModel(INavigationService navigationService, Environment environment)
    {
        Environment = environment;
        EditEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<EditEnvironmentPageViewModel>());
    }
}