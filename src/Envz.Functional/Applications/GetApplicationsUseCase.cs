using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Functional.Mediator;

namespace Envz.Functional.Applications;

public record GetApplicationsRequest : IRequest<IReadOnlyCollection<Application>>;

public class GetApplicationsUseCase(IApplicationRepository applicationRepository) : IUseCase<GetApplicationsRequest, IReadOnlyCollection<Application>>
{
    public IReadOnlyCollection<Application> Execute(GetApplicationsRequest parameter)
    {
        return applicationRepository.GetAll();
    }
}