using EnvBooster.UI.Utils;
using EnvBooster.UI.ViewModels.Pages.Environments.SubPages;

namespace EnvBooster.UI.ViewModels.Pages.Environments;

public class EnvironmentsPageViewModel : ViewModelBase
{
    public ViewModelBase CurrentSubViewModel
    {
        get => _currentSubViewModel;
        set
        {
            _currentSubViewModel = value;
            OnPropertyChanged();
        }
    }

    private ViewModelBase _currentSubViewModel = null!;
    private readonly HomeEnvironmentsSubPageViewModel _homeEnvironmentsSubPageViewModel;
    private readonly EditEnvironmentSubPageViewModel _editEnvironmentSubPageViewModel;

    public EnvironmentsPageViewModel(HomeEnvironmentsSubPageViewModel homeEnvironmentsSubPageViewModel, EditEnvironmentSubPageViewModel editEnvironmentSubPageViewModel)
    {
        _homeEnvironmentsSubPageViewModel = homeEnvironmentsSubPageViewModel;
        _editEnvironmentSubPageViewModel = editEnvironmentSubPageViewModel;

        CurrentSubViewModel = _homeEnvironmentsSubPageViewModel;

        Messenger.NavigationToCreateEnvironmentRequested += NavigateToCreateEnvironment;
        Messenger.NavigationToHomeEnvironmentsRequested += NavigateToHomeEnvironments;
    }

    public override void Dispose()
    {
        Messenger.NavigationToCreateEnvironmentRequested -= NavigateToCreateEnvironment;
        GC.SuppressFinalize(this);
    }

    private void NavigateToCreateEnvironment()
    {
        CurrentSubViewModel = _editEnvironmentSubPageViewModel;
    }

    private void NavigateToHomeEnvironments()
    {
        CurrentSubViewModel = _homeEnvironmentsSubPageViewModel;
    }
}