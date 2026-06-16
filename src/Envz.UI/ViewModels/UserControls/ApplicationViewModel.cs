using System.Windows.Media;

using Envz.Domain.Entities;
using Envz.UI.Services;

namespace Envz.UI.ViewModels.UserControls;

public class ApplicationViewModel(Application application, IIconExtractor iconExtractor) : ViewModelBase
{
    public Application Application { get; } = application;
    public ImageSource? Icon => Application.Icon?.Length > 0 ? iconExtractor.DecodeFromPngBytes(Application.Icon) : null;
}