using Envz.UI.Views.UserControls.EnvironmentItem;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services;

public class EnvironmentViewItemModelFactory(IServiceProvider serviceProvider)
{
    public EnvironmentItemViewModel Create(Environment environment)
        => ActivatorUtilities.CreateInstance<EnvironmentItemViewModel>(serviceProvider, environment);
}