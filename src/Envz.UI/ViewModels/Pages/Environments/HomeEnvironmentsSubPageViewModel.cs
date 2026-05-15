using System.Collections.ObjectModel;
using System.Windows.Input;

using Envz.Application.Environments;
using Envz.Application.Mediator;
using Envz.UI.Services;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.UserControls;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Environments;

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

    private readonly IMediator _mediator;
    private readonly EnvironmentViewModelFactory _environmentViewModelFactory;

    private IReadOnlyCollection<Environment> _environments = [];

    public HomeEnvironmentsSubPageViewModel(IMediator mediator, EnvironmentViewModelFactory environmentViewModelFactory, [FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService)
    {
        _mediator = mediator;
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
        _environments = _mediator.Send(new GetEnvironmentsRequest());
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
