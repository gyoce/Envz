using Envz.Common.Services;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using System.Windows.Media;

namespace Envz.UI.Views.UserControls.SelectableToolItem;

public class SelectableToolItemViewModel(Tool tool, IIconExtractor iconExtractor, bool isAlreadyAdded, Action<SelectableToolItemViewModel> onSelectionChanged) : ViewModelBase
{
    public Tool Tool { get; } = tool;
    public ImageSource? Icon => Tool.Icon?.Length > 0 ? iconExtractor.DecodeFromPngBytes(Tool.Icon) : null;
    public bool IsAlreadyAdded { get; } = isAlreadyAdded;
    public bool CanBeSelected => !IsAlreadyAdded;

    public bool IsSelected
    {
        get;
        set
        {
            if (field == value)
                return;
            field = value;
            OnPropertyChanged();
            onSelectionChanged(this);
        }
    }
}