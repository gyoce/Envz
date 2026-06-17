using Envz.Domain.Entities;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Views.UserControls.EnvironmentApplicationItem;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services;

public class EnvironmentApplicationViewModelFactory(IServiceProvider serviceProvider, IMediator mediator)
{
    public EnvironmentApplicationItemViewModel Create(EnvironmentApplication environmentApplication)
    {
        IReadOnlyCollection<Application> applications = mediator.Send(new GetApplicationsRequest());
        Application application = applications.First(a => a.Id == environmentApplication.ApplicationId);
        return ActivatorUtilities.CreateInstance<EnvironmentApplicationItemViewModel>(serviceProvider, environmentApplication, application);
    }
}
