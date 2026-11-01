using Envz.Common.Services;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using System.Windows.Input;
using System.Windows.Media;

namespace Envz.UI.Views.UserControls.ToolItem;

public readonly record struct ToolItemViewModelEvents(Action<Tool>? OnEdit = null, Action<Tool>? OnDelete = null);

public class ToolItemViewModel(Tool tool, IIconExtractor iconExtractor, ToolItemViewModelEvents events = new()) : ViewModelBase
{
    public Tool Tool { get; } = tool;
    public ImageSource? Icon => Tool.Icon?.Length > 0 ? iconExtractor.DecodeFromPngBytes(Tool.Icon) : null;
    public bool CanEdit => events.OnEdit is not null;
    public ICommand EditCommand { get; } = new RelayCommand(_ => events.OnEdit?.Invoke(tool), _ => events.OnEdit is not null);
    public bool CanDelete => events.OnDelete is not null;
    public ICommand DeleteCommand { get; } = new RelayCommand(_ => events.OnDelete?.Invoke(tool), _ => events.OnDelete is not null);
}