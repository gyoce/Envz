using Envz.Application.Mediator;
using Envz.Application.Requests.Tools;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.UI.Utils;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using Envz.UI.Services;
using Envz.UI.Views.UserControls.ToolItem;
using System.Windows.Input;

namespace Envz.UI.Views.Pages.Workspaces.SelectTool;

public class SelectToolPageViewModel : ResultPageViewModel<Tool>
{
    public override ENavigationCategory Category => ENavigationCategory.Workspaces;
    public override string Title => Strings.SelectTool_Title;
    public override int Level => 2;

    public ICommand SelectToolCommand { get; }
    public ICommand CancelCommand { get; }
    public SearchableCollection<ToolItemViewModel, Tool> SearchableTools { get; }
    public ToolItemViewModel? SelectedTool
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    private readonly IMediator _mediator;

    public SelectToolPageViewModel(IMediator mediator, ToolItemViewModelFactory viewModelFactory)
    {
        _mediator = mediator;

        SearchableTools = new SearchableCollection<ToolItemViewModel, Tool>(
            tool => tool.Name,
            tool => viewModelFactory.Create(tool)
        )
        {
            UnfilteredItems = _mediator.Send(new GetToolsRequest())
        };

        SelectToolCommand = new RelayCommand(_ => Complete(SelectedTool!.Tool), _ => SelectedTool is not null);
        CancelCommand = new RelayCommand(_ => Cancel());
    }

    public override void OnEnable()
    {
        SelectedTool = null;
        SearchableTools.SearchText = string.Empty;
        SearchableTools.UnfilteredItems = _mediator.Send(new GetToolsRequest());
    }
}