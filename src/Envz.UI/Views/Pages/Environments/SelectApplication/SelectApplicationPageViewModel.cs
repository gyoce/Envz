using System.Windows.Input;
using Envz.Common.Services.Navigation;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Utils;
using Envz.UI.Views.Pages.Environments.CreateEnvironment;
using Envz.UI.Views.UserControls.ApplicationItem;

namespace Envz.UI.Views.Pages.Environments.SelectApplication;

public class SelectApplicationPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Environments;
    public override string Title => "Select application";
    public override int Level => 2;

    public ICommand SelectApplicationCommand { get; }
    public ICommand CancelCommand { get; }
    public SearchableCollection<ApplicationItemViewModel, Application> SearchableApplications { get; }
    public ApplicationItemViewModel? SelectedApplication
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    private readonly IMediator _mediator;

    public SelectApplicationPageViewModel(IMediator mediator, ViewModelFactory viewModelFactory, INavigationService navigationService)
    {
        _mediator = mediator;

        SearchableApplications = new SearchableCollection<ApplicationItemViewModel, Application>(
            app => app.Name, 
            app => viewModelFactory.Create<ApplicationItemViewModel>(app)
        )
        {
            UnfilteredItems = _mediator.Send(new GetApplicationsRequest())
        };

        //SelectApplicationCommand = new RelayCommand(_ => SelectApplication(), _ => SelectedApplication is not null);
        CancelCommand = new RelayCommand(_ => navigationService.NavigateTo<CreateEnvironmentPageViewModel>());
    }

    public override void OnEnable()
    {
        SearchableApplications.UnfilteredItems = _mediator.Send(new GetApplicationsRequest());
    }
}