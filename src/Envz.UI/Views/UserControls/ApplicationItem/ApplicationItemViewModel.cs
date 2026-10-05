using Envz.Common.Services;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using System.Windows.Input;
using System.Windows.Media;

namespace Envz.UI.Views.UserControls.ApplicationItem;

public class ApplicationItemViewModel(Application application, IIconExtractor iconExtractor, Action<Application>? onDelete = null) : ViewModelBase
{
    public Application Application { get; } = application;
    public ImageSource? Icon => Application.Icon?.Length > 0 ? iconExtractor.DecodeFromPngBytes(Application.Icon) : null;
    public bool CanDelete => onDelete is not null;
    public ICommand DeleteCommand { get; } = new RelayCommand(_ => onDelete?.Invoke(application), _ => onDelete is not null);
}