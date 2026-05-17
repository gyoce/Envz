using System.Windows.Input;

using Envz.Domain.Entities;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.UserControls;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Applications;

public class HomeApplicationsSubPageViewModel : ViewModelBase
{
    public override string Title => "Home";
    public ICommand AddApplicationCommand { get; }
    public SearchableCollection<ApplicationViewModel, Application> SearchableApplications { get; }

    private readonly IMediator _mediator;

    public HomeApplicationsSubPageViewModel([FromKeyedServices(ENavigationRegion.Applications)] INavigationService navigationService, IMediator mediator, ApplicationViewModelFactory applicationViewModelFactory)
    {
        _mediator = mediator;
        AddApplicationCommand = new RelayCommand(_ => navigationService.NavigateTo<AddApplicationSubPageViewModel>());
        SearchableApplications = new SearchableCollection<ApplicationViewModel, Application>(app => app.Name, applicationViewModelFactory.Create);
        LoadApplications();
    }

    public override void OnEnable() 
    {
        LoadApplications();
    }

    private void LoadApplications()
    {
        SearchableApplications.UnfilteredItems = _mediator.Send(new GetApplicationsRequest());
    }
}