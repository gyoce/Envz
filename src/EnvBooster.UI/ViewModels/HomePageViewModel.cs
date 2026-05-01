using System.Collections.ObjectModel;

using EnvBooster.Application.Environments;
using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels;

public class HomePageViewModel : ViewModelBase
{
    public ObservableCollection<Environment> Environments { get; } = [];

    private readonly GetEnvironmentsUseCase _getEnvironmentsUseCase;

    public HomePageViewModel(GetEnvironmentsUseCase getEnvironmentsUseCase)
    {
        _getEnvironmentsUseCase = getEnvironmentsUseCase;
        Messenger.EnvironmentCreated += LoadEnvironments;
        LoadEnvironments();
    }

    public override void Dispose()
    {
        Messenger.EnvironmentCreated -= LoadEnvironments;
        GC.SuppressFinalize(this);
    }

    private void LoadEnvironments()
    {
        Environments.Clear();
        IReadOnlyCollection<Environment> environments = _getEnvironmentsUseCase.Execute();
        foreach (Environment environment in environments)
        {
            Environments.Add(environment);
        }
    }
}