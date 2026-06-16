using System.Windows.Input;

using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.Pages.Environments;

namespace Envz.UI.ViewModels.UserControls;

public class EnvironmentViewModel(INavigationService navigationService, Environment environment)
    : ViewModelBase
{
    public ICommand EditEnvironmentCommand { get; } = new RelayCommand(_ => navigationService.NavigateTo<EditEnvironmentPageViewModel>());
    public Environment Environment { get; } = environment;
    public int NumberOfApplications => Environment.Applications.Count;
}