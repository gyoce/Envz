using Envz.Common.Services;
using Envz.Common.Services.Navigation;
using Envz.Domain.Entities;
using Envz.UI.Views.UserControls.ApplicationItem;
using Envz.UI.Views.UserControls.EnvironmentApplicationItem;
using Envz.UI.Views.UserControls.EnvironmentItem;
using Envz.UI.Views.UserControls.SelectableApplicationItem;

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

public class SelectableApplicationItemViewModelFactory(IIconExtractor iconExtractor)
{
    public SelectableApplicationItemViewModel Create(SuggestedApplication suggestion, Action<SelectableApplicationItemViewModel> onSelectionChanged)
    {
        Application application = suggestion.Application;
        if (application.Icon is not { Length: > 0 })
            application = new Application(application.Name, application.Path, iconExtractor.ExtractPngBytes(application.Path));

        return new SelectableApplicationItemViewModel(application, iconExtractor, suggestion.IsAlreadyAdded, onSelectionChanged);
    }
}