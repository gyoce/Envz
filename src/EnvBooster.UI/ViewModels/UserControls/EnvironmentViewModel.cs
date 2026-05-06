using System.Windows.Input;

using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels.UserControls;

public class EnvironmentViewModel : ViewModelBase
{
    public Environment Environment { get; }
    public ICommand EditEnvironmentCommand { get; }

    public EnvironmentViewModel(Environment environment)
    {
        Environment = environment;
        EditEnvironmentCommand = new RelayCommand(_ => Messenger.NotifyNavigationToEditEnvironment(Environment));
    }
}