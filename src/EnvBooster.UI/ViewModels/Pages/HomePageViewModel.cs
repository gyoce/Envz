using System.Collections.ObjectModel;

using EnvBooster.Application.Environments;
using EnvBooster.UI.Utils;
using EnvBooster.UI.ViewModels.UserControls;

namespace EnvBooster.UI.ViewModels.Pages;

public class HomePageViewModel : ViewModelBase
{
    public ObservableCollection<EnvironmentViewModel> Environments { get; } = [];

    private readonly GetEnvironmentsUseCase _getEnvironmentsUseCase;
    private readonly EnvironmentViewModelFactory _environmentViewModelFactory;

    public HomePageViewModel(GetEnvironmentsUseCase getEnvironmentsUseCase, EnvironmentViewModelFactory environmentViewModelFactory)
    {
        _getEnvironmentsUseCase = getEnvironmentsUseCase;
        _environmentViewModelFactory = environmentViewModelFactory;
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
            Environments.Add(_environmentViewModelFactory.Create(environment));
        }
    }
}