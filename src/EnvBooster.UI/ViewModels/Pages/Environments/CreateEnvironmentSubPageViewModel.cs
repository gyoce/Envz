using System.Windows.Input;

using EnvBooster.Application.Environments;
using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels.Pages.Environments;

public class CreateEnvironmentSubPageViewModel : ViewModelBase
{
    public ICommand NavigateToHomeEnvironmentsCommand { get; }
    public ICommand CreateEnvironmentCommand { get; }
    public CreateEnvironmentRequest Request { get; set; } = new();

    public CreateEnvironmentSubPageViewModel(CreateEnvironmentUseCase createEnvironmentUseCase)
    {
        NavigateToHomeEnvironmentsCommand = new RelayCommand(_ => Messenger.NotifyNavigationToHomeEnvironments());
        CreateEnvironmentCommand = new RelayCommand(_ =>
            {
                createEnvironmentUseCase.Execute(Request);
                Messenger.NotifyEnvironmentCreated();
                Messenger.NotifyNavigationToHomeEnvironments();
            }
        );
    }
}
