using System.Windows.Input;

using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels.Pages.Environments.SubPages;

public class EditEnvironmentSubPageViewModel : ViewModelBase
{
    public ICommand NavigateToHomeEnvironmentsCommand { get; }

    public EditEnvironmentSubPageViewModel()
    {
        NavigateToHomeEnvironmentsCommand = new RelayCommand(_ => Messenger.NotifyNavigationToHomeEnvironments());
    }
}
