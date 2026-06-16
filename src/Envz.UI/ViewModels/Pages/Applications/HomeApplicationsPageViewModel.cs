using System.Windows.Input;

using Envz.Domain.Entities;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.UserControls;

namespace Envz.UI.ViewModels.Pages.Applications;

public class HomeApplicationsPageViewModel : PageViewModel
{
    public override string Title => "Home";
    public override Type ParentPageType => typeof(ApplicationsPageViewModel);

    public ICommand AddApplicationCommand { get; }
    public SearchableCollection<ApplicationViewModel, Application> SearchableApplications { get; }

    private readonly IMediator _mediator;

    public HomeApplicationsPageViewModel(INavigationService navigationService, IMediator mediator, ApplicationViewModelFactory applicationViewModelFactory)
    {
        _mediator = mediator;

        AddApplicationCommand = new RelayCommand(_ => navigationService.NavigateTo<AddApplicationPageViewModel>());
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