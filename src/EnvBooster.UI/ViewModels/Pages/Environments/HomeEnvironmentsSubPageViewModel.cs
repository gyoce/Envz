using System.Collections.ObjectModel;
using System.Windows.Input;

using EnvBooster.Application.Environments;
using EnvBooster.UI.Services;
using EnvBooster.UI.Services.Navigation;
using EnvBooster.UI.Utils;
using EnvBooster.UI.ViewModels.UserControls;

using Microsoft.Extensions.DependencyInjection;

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

    public HomeEnvironmentsSubPageViewModel(GetEnvironmentsUseCase getEnvironmentsUseCase, EnvironmentViewModelFactory environmentViewModelFactory, [FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService)
    {
        _getEnvironmentsUseCase = getEnvironmentsUseCase;
        _environmentViewModelFactory = environmentViewModelFactory;

        NavigateToCreateEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<CreateEnvironmentSubPageViewModel>());

        LoadEnvironments();
    }

    public override void OnEnable()
    {
        LoadEnvironments();
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
