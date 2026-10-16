using Envz.Common.Services;
using Envz.Common.ViewModels;
using System.Windows.Media;

namespace Envz.UI.Views.UserControls.SelectableApplicationItem;

public class SelectableApplicationItemViewModel(Application application, IIconExtractor iconExtractor, bool isAlreadyAdded, Action<SelectableApplicationItemViewModel> onSelectionChanged) : ViewModelBase
{
    public Application Application { get; } = application;
    public ImageSource? Icon => Application.Icon?.Length > 0 ? iconExtractor.DecodeFromPngBytes(Application.Icon) : null;
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