using Envz.Common.Services;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using System.Windows.Media;

namespace Envz.UI.Views.UserControls.WorkspaceToolItem;

public class WorkspaceToolItemViewModel(WorkspaceTool workspaceTool, Tool tool, IIconExtractor iconExtractor)
    : ViewModelBase
{
    public WorkspaceTool WorkspaceTool { get; } = workspaceTool;
    public Tool Tool { get; } = tool;
    public ImageSource? Icon => iconExtractor.DecodeFromPngBytes(Tool.Icon);
    public string ParameterText => string.IsNullOrEmpty(WorkspaceTool.Parameter)
        ? "Parameter: None"
        : $"Parameter: {WorkspaceTool.Parameter}";
}
