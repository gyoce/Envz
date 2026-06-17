using System.Windows.Media;

using Envz.Domain.Entities;
using Envz.UI.Services;

namespace Envz.UI.Views.UserControls.EnvironmentApplicationItem;

public class EnvironmentApplicationItemViewModel(EnvironmentApplication environmentApplication, Application application, IIconExtractor iconExtractor)
    : ViewModelBase
{
    public EnvironmentApplication EnvironmentApplication { get; } = environmentApplication;
    public Application Application { get; } = application;
    public ImageSource? Icon => iconExtractor.DecodeFromPngBytes(Application.Icon);
    public string ParameterText => string.IsNullOrEmpty(EnvironmentApplication.Parameter)
        ? "Parameter: None"
        : $"Parameter: {EnvironmentApplication.Parameter}";
}
