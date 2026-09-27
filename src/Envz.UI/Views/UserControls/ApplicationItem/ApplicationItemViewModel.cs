using Envz.UI.Services;
using System.Windows.Media;

namespace Envz.UI.Views.UserControls.ApplicationItem;

public class ApplicationItemViewModel(Application application, IIconExtractor iconExtractor) : ViewModelBase
{
    public Application Application { get; } = application;
    public ImageSource? Icon => Application.Icon?.Length > 0 ? iconExtractor.DecodeFromPngBytes(Application.Icon) : null;
}