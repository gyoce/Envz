using System.Collections.ObjectModel;
using System.Windows.Input;

using EnvBooster.Application.Environments;
using EnvBooster.UI.Utils;
using EnvBooster.UI.ViewModels.UserControls;

namespace EnvBooster.UI.ViewModels.Pages.Environments;

public class HomeEnvironmentsSubPageViewModel : ViewModelBase
{
    public ICommand NavigateToCreateEnvironmentCommand { get; }
    public ObservableCollection<EnvironmentViewModel> Environments { get; } = [];
    public string SearchText
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
            FilterEnvironments();
        }
    } = string.Empty;

    private readonly GetEnvironmentsUseCase _getEnvironmentsUseCase;
    private readonly EnvironmentViewModelFactory _environmentViewModelFactory;

    private IReadOnlyCollection<Environment> _environments = [];

    public HomeEnvironmentsSubPageViewModel(GetEnvironmentsUseCase getEnvironmentsUseCase, EnvironmentViewModelFactory environmentViewModelFactory)
    {
        _getEnvironmentsUseCase = getEnvironmentsUseCase;
        _environmentViewModelFactory = environmentViewModelFactory;

        NavigateToCreateEnvironmentCommand = new RelayCommand(_ => Messenger.NotifyNavigationToCreateEnvironment());

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
        _environments = _getEnvironmentsUseCase.Execute();
        FilterEnvironments();
    }

    private void FilterEnvironments()
    {
        Environments.Clear();

        IEnumerable<Environment> filteredEnvironments = string.IsNullOrWhiteSpace(SearchText)
            ? _environments
            : _environments.Where(env => env.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        foreach (Environment env in filteredEnvironments)
            Environments.Add(_environmentViewModelFactory.Create(env));
    }
}
