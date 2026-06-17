using System.Windows.Input;

using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.Views.Pages.Environments.EditEnvironment;

namespace Envz.UI.Views.UserControls.EnvironmentItem;

public class EnvironmentItemViewModel(INavigationService navigationService, Domain.Entities.Environment environment)
    : ViewModelBase
{
    public ICommand EditEnvironmentCommand { get; } = new RelayCommand(_ => navigationService.NavigateTo<EditEnvironmentPageViewModel>());
    public Domain.Entities.Environment Environment { get; } = environment;
    public int NumberOfApplications => Environment.Applications.Count;
}