using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

using Envz.Domain.Entities;
using Envz.Functional.Environments;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Services.Dialogs;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.Dialogs;
using Envz.UI.ViewModels.UserControls;
using Envz.UI.Views.Dialogs;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Environments;

public class CreateEnvironmentSubPageViewModel : ViewModelBase
{
    public override string Title => "Create environment";
    public ICommand CancelCreateEnvironmentCommand { get; }
    public ICommand CreateEnvironmentCommand { get; }
    public ICommand AddApplicationCommand { get; }
    public CreateEnvironmentRequest Request { get; set; } = new();
    public ObservableCollection<EnvironmentApplicationViewModel> ApplicationViewModels { get; } = [];
    public bool HasApplications => ApplicationViewModels.Count > 0;

    private readonly IMediator _mediator;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private readonly EnvironmentApplicationViewModelFactory _environmentApplicationViewModelFactory;

    public CreateEnvironmentSubPageViewModel(IMediator mediator, [FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService, IDialogService dialogService, EnvironmentApplicationViewModelFactory environmentApplicationViewModelFactory)
    {
        _mediator = mediator;
        _navigationService = navigationService;
        _dialogService = dialogService;
        _environmentApplicationViewModelFactory = environmentApplicationViewModelFactory;
        CreateEnvironmentCommand = new RelayCommand(_ => CreateEnvironment(), _ => CanCreateEnvironment());
        CancelCreateEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<HomeEnvironmentsSubPageViewModel>());
        AddApplicationCommand = new RelayCommand(_ => AddApplication());
        ApplicationViewModels.CollectionChanged += OnApplicationViewModelsChanged;
    }

    private void OnApplicationViewModelsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(HasApplications));
    }

    private void CreateEnvironment()
    {
        _mediator.Send(Request);
        _navigationService.NavigateTo<HomeEnvironmentsSubPageViewModel>();
    }

    private bool CanCreateEnvironment()
    {
        return !string.IsNullOrWhiteSpace(Request.Name) && Request.Applications.Count > 0;
    }

    private void AddApplication()
    {
        EnvironmentApplication? application = _dialogService.ShowDialog<SelectApplicationDialog, SelectApplicationDialogViewModel, EnvironmentApplication>();
        if (application is null)
            return;

        Request.Applications.Add(application);
        ApplicationViewModels.Add(_environmentApplicationViewModelFactory.Create(application));
    }
}
