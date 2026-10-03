using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.UI.Utils;
using Envz.Common.Utils;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Views.UserControls.ApplicationItem;
using System.Windows.Input;

namespace Envz.UI.Views.Pages.Environments.SelectApplication;

public class SelectApplicationPageViewModel : ResultPageViewModel<Application>
{
    public override ENavigationCategory Category => ENavigationCategory.Environments;
    public override string Title => Strings.SelectApplication_Title;
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

    public SelectApplicationPageViewModel(IMediator mediator, ApplicationItemViewModelFactory viewModelFactory)
    {
        _mediator = mediator;

        SearchableApplications = new SearchableCollection<ApplicationItemViewModel, Application>(
            app => app.Name,
            app => viewModelFactory.Create(app)
        )
        {
            UnfilteredItems = _mediator.Send(new GetApplicationsRequest())
        };

        SelectApplicationCommand = new RelayCommand(_ => Complete(SelectedApplication!.Application), _ => SelectedApplication is not null);
        CancelCommand = new RelayCommand(_ => Cancel());
    }

    public override void OnEnable()
    {
        SelectedApplication = null;
        SearchableApplications.SearchText = string.Empty;
        SearchableApplications.UnfilteredItems = _mediator.Send(new GetApplicationsRequest());
    }
}