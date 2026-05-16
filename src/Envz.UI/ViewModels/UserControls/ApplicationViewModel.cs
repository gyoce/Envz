using System.Windows.Media;

using Envz.Domain.Entities;
using Envz.UI.Services;

namespace Envz.UI.ViewModels.UserControls;

public class ApplicationViewModel : ViewModelBase
{
    public Application Application { get; }
    public ImageSource? Icon => Application.Icon.Length > 0 ? _iconExtractor.DecodeFromPngBytes(Application.Icon) : null;

    private readonly IIconExtractor _iconExtractor;

    public ApplicationViewModel(Application application, IIconExtractor iconExtractor)
    {
        Application = application;
        _iconExtractor = iconExtractor;
    }
}