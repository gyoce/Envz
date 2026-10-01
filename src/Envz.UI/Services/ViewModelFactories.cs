using Envz.Common.Services;
using Envz.Common.Services.Navigation;
using Envz.Domain.Entities;
using Envz.UI.Views.UserControls.ApplicationItem;
using Envz.UI.Views.UserControls.EnvironmentApplicationItem;
using Envz.UI.Views.UserControls.EnvironmentItem;

namespace Envz.UI.Services;

public class ApplicationItemViewModelFactory(IIconExtractor iconExtractor)
{
    public ApplicationItemViewModel Create(Application application, Action<Application>? onDelete = null)
        => new(application, iconExtractor, onDelete);
}

public class EnvironmentApplicationItemViewModelFactory(IIconExtractor iconExtractor)
{
    public EnvironmentApplicationItemViewModel Create(EnvironmentApplication environmentApplication, Application application)
        => new(environmentApplication, application, iconExtractor);
}

public class EnvironmentItemViewModelFactory(INavigationService navigationService)
{
    public EnvironmentItemViewModel Create(Environment environment)
        => new(navigationService, environment);
}