using Envz.Domain.Entities;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.ViewModels.UserControls;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services;

public class EnvironmentApplicationViewModelFactory(IServiceProvider serviceProvider, IMediator mediator)
{
    public EnvironmentApplicationViewModel Create(EnvironmentApplication environmentApplication)
    {
        IReadOnlyCollection<Application> applications = mediator.Send(new GetApplicationsRequest());
        Application application = applications.First(a => a.Id == environmentApplication.ApplicationId);
        return ActivatorUtilities.CreateInstance<EnvironmentApplicationViewModel>(serviceProvider, environmentApplication, application);
    }
}
