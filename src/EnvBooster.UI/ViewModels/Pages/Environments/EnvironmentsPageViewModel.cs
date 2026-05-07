using EnvBooster.Application.Environments;
using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels.Pages.Environments;

public class EnvironmentsPageViewModel : ViewModelBase
{
    public ViewModelBase CurrentSubViewModel
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    private readonly HomeEnvironmentsSubPageViewModel _homeEnvironmentsSubPageViewModel;
    private readonly EditEnvironmentSubPageViewModel _editEnvironmentSubPageViewModel;
    private readonly CreateEnvironmentSubPageViewModel _createEnvironmentSubPageViewModel;

    public EnvironmentsPageViewModel(HomeEnvironmentsSubPageViewModel homeEnvironmentsSubPageViewModel, EditEnvironmentSubPageViewModel editEnvironmentSubPageViewModel, CreateEnvironmentSubPageViewModel createEnvironmentSubPageViewModel)
    {
        _homeEnvironmentsSubPageViewModel = homeEnvironmentsSubPageViewModel;
        _editEnvironmentSubPageViewModel = editEnvironmentSubPageViewModel;
        _createEnvironmentSubPageViewModel = createEnvironmentSubPageViewModel;

        CurrentSubViewModel = _homeEnvironmentsSubPageViewModel;

        Messenger.NavigationToHomeEnvironmentsRequested += NavigateToHomeEnvironments;
        Messenger.NavigationToCreateEnvironmentRequested += NavigateToCreateEnvironment;
        Messenger.NavigationToEditEnvironmentRequest += NavigateToEditEnvironment;
    }

    public override void Dispose()
    {
        Messenger.NavigationToHomeEnvironmentsRequested -= NavigateToHomeEnvironments;
        Messenger.NavigationToCreateEnvironmentRequested -= NavigateToCreateEnvironment;
        Messenger.NavigationToEditEnvironmentRequest -= NavigateToEditEnvironment;
        GC.SuppressFinalize(this);
    }

    private void NavigateToHomeEnvironments()
    {
        CurrentSubViewModel = _homeEnvironmentsSubPageViewModel;
    }

    private void NavigateToCreateEnvironment()
    {
        CurrentSubViewModel = _createEnvironmentSubPageViewModel;
        _createEnvironmentSubPageViewModel.Request = new CreateEnvironmentRequest();
    }

    private void NavigateToEditEnvironment(Environment environment)
    {
        CurrentSubViewModel = _editEnvironmentSubPageViewModel;
        _editEnvironmentSubPageViewModel.Environment = environment;
    }
}