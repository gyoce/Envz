using System.Windows.Input;

using EnvBooster.Application.Environments;
using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels.Pages;

public class CreateEnvironmentPageViewModel : ViewModelBase
{
    public ICommand CreateEnvironmentCommand { get; }

    private readonly CreateEnvironmentUseCase _createEnvironmentUseCase;

    public CreateEnvironmentPageViewModel(CreateEnvironmentUseCase createEnvironmentUseCase)
    {
        _createEnvironmentUseCase = createEnvironmentUseCase;
        CreateEnvironmentCommand = new RelayCommand(_ => CreateEnvironment());
    }

    private void CreateEnvironment()
    {
        _createEnvironmentUseCase.Execute(Random.Shared.NextInt64().ToString());
        Messenger.NotifyEnvironmentCreated();
    }
}