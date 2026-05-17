using System.Windows.Media;

using Envz.Domain.Entities;
using Envz.UI.Services;

namespace Envz.UI.ViewModels.UserControls;

public class EnvironmentApplicationViewModel : ViewModelBase
{
    public EnvironmentApplication EnvironmentApplication { get; }
    public Application Application { get; }
    public ImageSource? Icon => _iconExtractor.DecodeFromPngBytes(Application.Icon);
    public string ParameterText => string.IsNullOrEmpty(EnvironmentApplication.Parameter)
        ? "Parameter: None"
        : $"Parameter: {EnvironmentApplication.Parameter}";

    private readonly IIconExtractor _iconExtractor;

    public EnvironmentApplicationViewModel(EnvironmentApplication environmentApplication, Application application, IIconExtractor iconExtractor)
    {
        EnvironmentApplication = environmentApplication;
        Application = application;
        _iconExtractor = iconExtractor;
    }
}
