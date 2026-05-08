using System.Collections.ObjectModel;
using System.Windows.Input;

using EnvBooster.Application.Environments;
using EnvBooster.UI.Utils;
using EnvBooster.UI.ViewModels.UserControls;

namespace EnvBooster.UI.ViewModels.Pages.Environments;

public class CreateEnvironmentSubPageViewModel : ViewModelBase
{
    public ICommand CreateEnvironmentCommand { get; }
    public ICommand CancelCreateEnvironmentCommand { get; }
    public ObservableCollection<EnvironmentApplicationViewModel> EnvironmentApplications { get; } = [];
    public CreateEnvironmentRequest Request { get; set; } = new();

    public CreateEnvironmentSubPageViewModel(CreateEnvironmentUseCase createEnvironmentUseCase)
    {
        CancelCreateEnvironmentCommand = new RelayCommand(_ =>
        {
            Request = new CreateEnvironmentRequest();
            Messenger.NotifyNavigationToHomeEnvironments();
        });
        CreateEnvironmentCommand = new RelayCommand(_ =>
        {
            createEnvironmentUseCase.Execute(Request);
            Messenger.NotifyEnvironmentCreated();
            Messenger.NotifyNavigationToHomeEnvironments();
        });
    }
}
