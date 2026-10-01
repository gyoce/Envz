using Envz.Common.Services.Navigation;
using Envz.Common.Utils;
using Envz.Domain.Entities;
using Envz.Functional.Environments;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Views.Pages.Environments.HomeEnvironments;
using Envz.UI.Views.Pages.Environments.SelectApplication;
using Envz.UI.Views.UserControls.EnvironmentApplicationItem;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace Envz.UI.Views.Pages.Environments.CreateEnvironment;

public class CreateEnvironmentPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Environments;
    public override string Title => "Create environment";

    public ICommand CancelCreateEnvironmentCommand { get; }
    public ICommand CreateEnvironmentCommand { get; }
    public ICommand AddApplicationCommand { get; }
    public CreateEnvironmentRequest CreateEnvironmentRequest { get; set; } = new();
    public ObservableCollection<EnvironmentApplicationItemViewModel> ApplicationViewModels { get; } = [];
    public bool HasApplications => ApplicationViewModels.Count > 0;

    private readonly IMediator _mediator;
    private readonly INavigationService _navigationService;
    private readonly EnvironmentApplicationItemViewModelFactory _viewModelFactory;

    public CreateEnvironmentPageViewModel(IMediator mediator, INavigationService navigationService, EnvironmentApplicationItemViewModelFactory viewModelFactory)
    {
        _mediator = mediator;
        _navigationService = navigationService;
        _viewModelFactory = viewModelFactory;

        CreateEnvironmentCommand = new RelayCommand(_ => CreateEnvironment(), _ => CanCreateEnvironment());
        CancelCreateEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<HomeEnvironmentsPageViewModel>());
        AddApplicationCommand = new AsyncRelayCommand(async _ => await AddApplicationAsync());
        ApplicationViewModels.CollectionChanged += OnApplicationViewModelsChanged;
    }

    private void OnApplicationViewModelsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(HasApplications));
    }

    private void CreateEnvironment()
    {
        _mediator.Send(CreateEnvironmentRequest);
        _navigationService.NavigateTo<HomeEnvironmentsPageViewModel>();
    }

    private bool CanCreateEnvironment()
    {
        return !string.IsNullOrWhiteSpace(CreateEnvironmentRequest.Name) && CreateEnvironmentRequest.Applications.Count > 0;
    }

    private async Task AddApplicationAsync()
    {
        Application? application = await _navigationService.NavigateForResultAsync<SelectApplicationPageViewModel, Application>();
        if (application is not null)
        {
            EnvironmentApplication environmentApplication = new() { ApplicationName = application.Name };
            CreateEnvironmentRequest.Applications.Add(environmentApplication);
            ApplicationViewModels.Add(_viewModelFactory.Create(environmentApplication, application));
        }
    }

    public override void Dispose()
    {
        ApplicationViewModels.CollectionChanged -= OnApplicationViewModelsChanged;
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
