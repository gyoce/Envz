using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Functional.Mediator;

namespace Envz.Functional.Applications;

public record GetSuggestedApplicationsRequest : IRequest<IReadOnlyCollection<SuggestedApplication>>;

public class GetSuggestedApplicationsUseCase(IInstalledApplicationProvider installedApplicationProvider, IApplicationRepository applicationRepository) 
    : IAsyncUseCase<GetSuggestedApplicationsRequest, IReadOnlyCollection<SuggestedApplication>>
{
    public async Task<IReadOnlyCollection<SuggestedApplication>> ExecuteAsync(GetSuggestedApplicationsRequest request)
    {
        IReadOnlyCollection<Application> existing = applicationRepository.GetAll();

        return (await installedApplicationProvider.GetInstalledAsync())
            .Select(app => new SuggestedApplication(app, IsAlreadyAdded(app, existing)))
            .OrderBy(suggestion => suggestion.IsAlreadyAdded)
            .ThenBy(suggestion => suggestion.Application.Name, StringComparer.CurrentCultureIgnoreCase)
            .ToList();
    }

    private static bool IsAlreadyAdded(Application app, IReadOnlyCollection<Application> existing)
    {
        return existing.Any(application => application.Name == app.Name || string.Equals(application.Path, app.Path, StringComparison.OrdinalIgnoreCase));
    }
}