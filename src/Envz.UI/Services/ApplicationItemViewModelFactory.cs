using Envz.UI.Views.UserControls.ApplicationItem;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services;

public class ApplicationItemViewModelFactory(IServiceProvider serviceProvider)
{
    public ApplicationItemViewModel Create(Application application)
        => ActivatorUtilities.CreateInstance<ApplicationItemViewModel>(serviceProvider, application);
}