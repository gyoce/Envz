using System.Windows.Input;

using EnvBooster.Application.Environments;
using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels.Pages.Environments.SubPages;

public class CreateEnvironmentSubPageViewModel : ViewModelBase
{
    public ICommand NavigateToHomeEnvironmentsCommand { get; }
    public CreateEnvironmentRequest Request { get; set; } = new();

    public CreateEnvironmentSubPageViewModel()
    {
        NavigateToHomeEnvironmentsCommand = new RelayCommand(_ => Messenger.NotifyNavigationToHomeEnvironments());
    }
}
