using Envz.Common.Services;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using System.Windows.Input;
using System.Windows.Media;

namespace Envz.UI.Views.UserControls.ToolItem;

public class ToolItemViewModel(Tool tool, IIconExtractor iconExtractor, Action<Tool>? onDelete = null) : ViewModelBase
{
    public Tool Tool { get; } = tool;
    public ImageSource? Icon => Tool.Icon?.Length > 0 ? iconExtractor.DecodeFromPngBytes(Tool.Icon) : null;
    public bool CanDelete => onDelete is not null;
    public ICommand DeleteCommand { get; } = new RelayCommand(_ => onDelete?.Invoke(tool), _ => onDelete is not null);
}