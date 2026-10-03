using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.UI.Utils;
using Envz.Common.Utils;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Views.Pages.Applications.AddApplication;
using Envz.UI.Views.UserControls.ApplicationItem;
using System.Windows.Input;

namespace Envz.UI.Views.Pages.Applications.HomeApplications;

public class HomeApplicationsPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Applications;
    public override string Title => Strings.HomeApplications_Title;

    public ICommand AddApplicationCommand { get; }
    public SearchableCollection<ApplicationItemViewModel, Application> SearchableApplications { get; }

    private readonly IMediator _mediator;

    public HomeApplicationsPageViewModel(INavigationService navigationService, IMediator mediator, ApplicationItemViewModelFactory viewModelFactory)
    {
        _mediator = mediator;

        AddApplicationCommand = new RelayCommand(_ => navigationService.NavigateTo<AddApplicationPageViewModel>());
        SearchableApplications = new SearchableCollection<ApplicationItemViewModel, Application>(
            app => app.Name,
            app => viewModelFactory.Create(app, DeleteApplication)
        );

        LoadApplications();
    }

    private void DeleteApplication(Application app)
    {
        _mediator.Send(new DeleteApplicationRequest
        {
            ApplicationName = app.Name
        });
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