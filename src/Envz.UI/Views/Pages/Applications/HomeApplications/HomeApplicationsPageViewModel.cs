using System.Windows.Input;

using Envz.Common.Services.Navigation;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Utils;
using Envz.UI.Views.Pages.Applications.AddApplication;
using Envz.UI.Views.UserControls.ApplicationItem;

namespace Envz.UI.Views.Pages.Applications.HomeApplications;

public class HomeApplicationsPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Applications;
    public override string Title => "Home";

    public ICommand AddApplicationCommand { get; }
    public SearchableCollection<ApplicationItemViewModel, Application> SearchableApplications { get; }

    private readonly IMediator _mediator;

    public HomeApplicationsPageViewModel(INavigationService navigationService, IMediator mediator, ViewModelFactory viewModelFactory)
    {
        _mediator = mediator;

        AddApplicationCommand = new RelayCommand(_ => navigationService.NavigateTo<AddApplicationPageViewModel>());
        SearchableApplications = new SearchableCollection<ApplicationItemViewModel, Application>(
            app => app.Name,
            app => viewModelFactory.Create<ApplicationItemViewModel>(app)
        );

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