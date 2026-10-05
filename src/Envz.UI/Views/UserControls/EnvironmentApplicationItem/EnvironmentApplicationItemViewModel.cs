using Envz.Common.Services;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using System.Windows.Media;

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
