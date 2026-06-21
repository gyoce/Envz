using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Envz.Common.Services.Navigation;
using Envz.Functional.Environments;
using Envz.Functional.Mediator;
using Envz.UI.Services.Dialogs;
using Envz.UI.Utils;
using Envz.UI.Views.Pages.Environments.HomeEnvironments;
using Envz.UI.Views.Pages.Environments.SelectApplication;
using Envz.UI.Views.UserControls.EnvironmentApplicationItem;

namespace Envz.UI.Views.Pages.Environments.CreateEnvironment;

public class CreateEnvironmentPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Environments;
    public override string Title => "Create environment";

    public ICommand CancelCreateEnvironmentCommand { get; }
    public ICommand CreateEnvironmentCommand { get; }
    public ICommand AddApplicationCommand { get; }
    public CreateEnvironmentRequest Request { get; set; } = new();
    public ObservableCollection<EnvironmentApplicationItemViewModel> ApplicationViewModels { get; } = [];
    public bool HasApplications => ApplicationViewModels.Count > 0;

    private readonly IMediator _mediator;
    private readonly INavigationService _navigationService;

    public CreateEnvironmentPageViewModel(IMediator mediator, INavigationService navigationService, IDialogService dialogService)
    {
        _mediator = mediator;
        _navigationService = navigationService;

        CreateEnvironmentCommand = new RelayCommand(_ => CreateEnvironment(), _ => CanCreateEnvironment());
        CancelCreateEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<HomeEnvironmentsPageViewModel>());
        AddApplicationCommand = new RelayCommand(_ => navigationService.NavigateTo<SelectApplicationPageViewModel>());
        ApplicationViewModels.CollectionChanged += OnApplicationViewModelsChanged;
    }

    private void OnApplicationViewModelsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(HasApplications));
    }

    private void CreateEnvironment()
    {
        _mediator.Send(Request);
        _navigationService.NavigateTo<HomeEnvironmentsPageViewModel>();
    }

    private bool CanCreateEnvironment()
    {
        return !string.IsNullOrWhiteSpace(Request.Name) && Request.Applications.Count > 0;
    }

    private void AddApplication()
    {
        //EnvironmentApplication? application = _dialogService.ShowDialog<SelectApplicationDialog, SelectApplicationDialogViewModel, EnvironmentApplication>();
        //if (application is null)
        //    return;

        //Request.Applications.Add(application);
        //ApplicationViewModels.Add(_environmentApplicationViewModelFactory.Create(application));
    }

    public override void Dispose()
    {
        ApplicationViewModels.CollectionChanged -= OnApplicationViewModelsChanged;
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
