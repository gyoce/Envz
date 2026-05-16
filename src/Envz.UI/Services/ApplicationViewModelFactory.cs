using Envz.UI.ViewModels.UserControls;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services;

public class ApplicationViewModelFactory(IServiceProvider serviceProvider)
{
    public ApplicationViewModel Create(Domain.Entities.Application application)
        => ActivatorUtilities.CreateInstance<ApplicationViewModel>(serviceProvider, application);
}