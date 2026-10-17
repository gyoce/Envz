using Envz.Application.Mediator;
using Envz.Application.Requests.Workspaces;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.UI.Utils;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using Envz.UI.Services;
using Envz.UI.Views.Pages.Workspaces.CreateWorkspace;
using Envz.UI.Views.UserControls.WorkspaceItem;
using System.Windows.Input;

namespace Envz.UI.Views.Pages.Workspaces.HomeWorkspaces;

public class HomeWorkspacesPageViewModel : PageViewModel
{
    public override string Title => Strings.HomeWorkspaces_Title;
    public override ENavigationCategory Category => ENavigationCategory.Workspaces;

    public ICommand NavigateToCreateWorkspaceCommand { get; }
    public SearchableCollection<WorkspaceItemViewModel, Workspace> SearchableWorkspaces { get; }

    private readonly IMediator _mediator;

    public HomeWorkspacesPageViewModel(IMediator mediator, WorkspaceItemViewModelFactory viewModelFactory, INavigationService navigationService)
    {
        _mediator = mediator;

        NavigateToCreateWorkspaceCommand = new RelayCommand(_ => navigationService.NavigateTo<CreateWorkspacePageViewModel>());
        SearchableWorkspaces = new SearchableCollection<WorkspaceItemViewModel, Workspace>(
            workspace => workspace.Name,
            workspace => viewModelFactory.Create(workspace)
        );

        LoadWorkspaces();
    }

    public override void OnEnable()
    {
        LoadWorkspaces();
    }

    private void LoadWorkspaces()
    {
        SearchableWorkspaces.UnfilteredItems = _mediator.Send(new GetWorkspacesRequest());
    }
}
