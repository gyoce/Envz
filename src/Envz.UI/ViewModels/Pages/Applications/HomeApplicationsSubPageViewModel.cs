using System.Collections.ObjectModel;
using System.Windows.Input;

using Envz.Application.Applications;
using Envz.Application.Mediator;
using Envz.UI.Services;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.UserControls;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Applications;

public class HomeApplicationsSubPageViewModel : ViewModelBase
{
    public ICommand AddApplicationCommand { get; }
    public ObservableCollection<ApplicationViewModel> Applications { get; } = [];
    public string SearchText
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
            FilterApplications();
        }
    } = string.Empty;

    private readonly IMediator _mediator;
    private readonly ApplicationViewModelFactory _applicationViewModelFactory;
    private IReadOnlyCollection<Domain.Entities.Application> _applications = [];

    public HomeApplicationsSubPageViewModel([FromKeyedServices(ENavigationRegion.Applications)] INavigationService navigationService, IMediator mediator, ApplicationViewModelFactory applicationViewModelFactory)
    {
        _mediator = mediator;
        _applicationViewModelFactory = applicationViewModelFactory;
        AddApplicationCommand = new RelayCommand(_ => navigationService.NavigateTo<AddApplicationSubPageViewModel>());

        LoadApplications();
    }

    public override void OnEnable()
    {
        LoadApplications();
    }

    private void LoadApplications()
    {
        _applications = _mediator.Send(new GetApplicationsRequest());
        FilterApplications();
    }

    private void FilterApplications()
    {
        Applications.Clear();

        IEnumerable<Domain.Entities.Application> filteredApplications = string.IsNullOrWhiteSpace(SearchText)
            ? _applications
            : _applications.Where(app => app.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        foreach (Domain.Entities.Application app in filteredApplications)
            Applications.Add(_applicationViewModelFactory.Create(app));
    }
}