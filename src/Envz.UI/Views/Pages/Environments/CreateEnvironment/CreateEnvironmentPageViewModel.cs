using Envz.Common.Services.Navigation;
using Envz.Functional.Environments;
using Envz.Functional.Mediator;
using Envz.UI.Utils;
using Envz.UI.Views.Pages.Environments.HomeEnvironments;
using Envz.UI.Views.Pages.Environments.SelectApplication;
using Envz.UI.Views.UserControls.ApplicationItem;
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

    private ApplicationItemViewModel? _selectedApplication;

    private readonly IMediator _mediator;
    private readonly INavigationService _navigationService;

    public CreateEnvironmentPageViewModel(IMediator mediator, INavigationService navigationService)
    {
        _mediator = mediator;
        _navigationService = navigationService;

        CreateEnvironmentCommand = new RelayCommand(_ => CreateEnvironment(), _ => CanCreateEnvironment());
        CancelCreateEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<HomeEnvironmentsPageViewModel>());
        AddApplicationCommand = new RelayCommand(_ => AddApplication());
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

    private void AddApplication()
    {
        _navigationService.NavigateTo<SelectApplicationPageViewModel>(selectApplicationviewModel =>
        {
            _selectedApplication = selectApplicationviewModel.SelectedApplication;
            if (_selectedApplication is not null)
            {

            }
        });
    }

    public override void Dispose()
    {
        ApplicationViewModels.CollectionChanged -= OnApplicationViewModelsChanged;
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
