using Envz.Application.Mediator;
using Envz.Application.Requests.Workspaces;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using Envz.UI.Services;
using Envz.UI.Views.Pages.Workspaces.HomeWorkspaces;
using Envz.UI.Views.Pages.Workspaces.SelectTool;
using Envz.UI.Views.UserControls.WorkspaceToolItem;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace Envz.UI.Views.Pages.Workspaces.CreateWorkspace;

public class CreateWorkspacePageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Workspaces;
    public override string Title => Strings.CreateWorkspace_Title;

    public ICommand CancelCreateWorkspaceCommand { get; }
    public ICommand CreateWorkspaceCommand { get; }
    public ICommand AddToolCommand { get; }
    public CreateWorkspaceRequest CreateWorkspaceRequest { get; set; } = new();
    public ObservableCollection<WorkspaceToolItemViewModel> ToolViewModels { get; } = [];
    public bool HasTools => ToolViewModels.Count > 0;

    private readonly IMediator _mediator;
    private readonly INavigationService _navigationService;
    private readonly WorkspaceToolItemViewModelFactory _viewModelFactory;

    private bool _isSelectingTool = false;

    public CreateWorkspacePageViewModel(IMediator mediator, INavigationService navigationService, WorkspaceToolItemViewModelFactory viewModelFactory)
    {
        _mediator = mediator;
        _navigationService = navigationService;
        _viewModelFactory = viewModelFactory;

        CreateWorkspaceCommand = new RelayCommand(_ => CreateWorkspace(), _ => CanCreateWorkspace());
        CancelCreateWorkspaceCommand = new RelayCommand(_ => navigationService.NavigateTo<HomeWorkspacesPageViewModel>());
        AddToolCommand = new AsyncRelayCommand(async _ => await AddToolAsync());
        ToolViewModels.CollectionChanged += OnToolViewModelsChanged;
    }

    public override void OnEnable()
    {
        if (_isSelectingTool)
            return;

        CreateWorkspaceRequest = new CreateWorkspaceRequest();
        OnPropertyChanged(nameof(CreateWorkspaceRequest));
        ToolViewModels.Clear();
    }

    private void OnToolViewModelsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(HasTools));
    }

    private void CreateWorkspace()
    {
        _mediator.Send(CreateWorkspaceRequest);
        _navigationService.NavigateTo<HomeWorkspacesPageViewModel>();
    }

    private bool CanCreateWorkspace()
    {
        return !string.IsNullOrWhiteSpace(CreateWorkspaceRequest.Name) && CreateWorkspaceRequest.Tools.Count > 0;
    }

    private async Task AddToolAsync()
    {
        _isSelectingTool = true;
        Tool? tool;
        try
        {
            tool = await _navigationService.NavigateForResultAsync<SelectToolPageViewModel, Tool>();
        }
        finally
        {
            _isSelectingTool = false;
        }

        if (tool is not null)
        {
            WorkspaceTool workspaceTool = new(tool.Name);
            CreateWorkspaceRequest.Tools.Add(workspaceTool);
            ToolViewModels.Add(_viewModelFactory.Create(workspaceTool, tool));
        }
    }

    public override void Dispose()
    {
        ToolViewModels.CollectionChanged -= OnToolViewModelsChanged;
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
