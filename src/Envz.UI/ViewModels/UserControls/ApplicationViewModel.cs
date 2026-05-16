using System.Windows.Media;

using Envz.UI.Services;

namespace Envz.UI.ViewModels.UserControls;

public class ApplicationViewModel : ViewModelBase
{
    public Domain.Entities.Application Application { get; }
    public ImageSource? Icon => Application.Icon.Length > 0 ? _iconExtractor.DecodeFromPngBytes(Application.Icon) : null;

    private readonly IIconExtractor _iconExtractor;

    public ApplicationViewModel(Domain.Entities.Application application, IIconExtractor iconExtractor)
    {
        Application = application;
        _iconExtractor = iconExtractor;
    }
}