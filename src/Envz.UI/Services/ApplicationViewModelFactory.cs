using Envz.Domain.Entities;
using Envz.UI.ViewModels.UserControls;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services;

public class ApplicationViewModelFactory(IServiceProvider serviceProvider)
{
    public ApplicationViewModel Create(Application application)
        => ActivatorUtilities.CreateInstance<ApplicationViewModel>(serviceProvider, application);
}