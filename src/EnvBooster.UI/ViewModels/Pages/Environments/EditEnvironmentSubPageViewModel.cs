using System.Windows.Input;

using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels.Pages.Environments;

public class EditEnvironmentSubPageViewModel : ViewModelBase
{
    public ICommand NavigateToHomeEnvironmentsCommand { get; }
    public Environment Environment { get; set; } = null!;

    public EditEnvironmentSubPageViewModel()
    {
        NavigateToHomeEnvironmentsCommand = new RelayCommand(_ => Messenger.NotifyNavigationToHomeEnvironments());
    }
}
