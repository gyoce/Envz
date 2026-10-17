using Envz.Common.Services;
using Envz.Common.Services.Navigation;
using Envz.Domain.Entities;
using Envz.UI.Views.UserControls.SelectableToolItem;
using Envz.UI.Views.UserControls.ToolItem;
using Envz.UI.Views.UserControls.WorkspaceItem;
using Envz.UI.Views.UserControls.WorkspaceToolItem;

namespace Envz.UI.Services;

public class ToolItemViewModelFactory(IIconExtractor iconExtractor)
{
    public ToolItemViewModel Create(Tool tool, Action<Tool>? onDelete = null)
        => new(tool, iconExtractor, onDelete);
}

public class WorkspaceToolItemViewModelFactory(IIconExtractor iconExtractor)
{
    public WorkspaceToolItemViewModel Create(WorkspaceTool workspaceTool, Tool tool)
        => new(workspaceTool, tool, iconExtractor);
}

public class WorkspaceItemViewModelFactory(INavigationService navigationService)
{
    public WorkspaceItemViewModel Create(Workspace workspace)
        => new(navigationService, workspace);
}

public class SelectableToolItemViewModelFactory(IIconExtractor iconExtractor)
{
    public SelectableToolItemViewModel Create(SuggestedTool suggestion, Action<SelectableToolItemViewModel> onSelectionChanged)
    {
        Tool tool = suggestion.Tool;
        if (tool.Icon is not { Length: > 0 })
            tool = new Tool(tool.Name, tool.Path, iconExtractor.ExtractPngBytes(tool.Path));

        return new SelectableToolItemViewModel(tool, iconExtractor, suggestion.IsAlreadyAdded, onSelectionChanged);
    }
}