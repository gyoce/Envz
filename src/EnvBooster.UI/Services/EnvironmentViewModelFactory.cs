using EnvBooster.UI.ViewModels.UserControls;

using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI.Services;

public class EnvironmentViewModelFactory(IServiceProvider serviceProvider)
{
    public EnvironmentViewModel Create(Environment environment)
        => ActivatorUtilities.CreateInstance<EnvironmentViewModel>(serviceProvider, environment);
}