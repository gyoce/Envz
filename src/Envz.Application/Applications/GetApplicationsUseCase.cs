using Envz.Application.Mediator;
using Envz.Domain.Ports;

namespace Envz.Application.Applications;

public record GetApplicationsRequest : IRequest<IReadOnlyCollection<Domain.Entities.Application>>;

public class GetApplicationsUseCase(IApplicationRepository applicationRepository) : IUseCase<GetApplicationsRequest, IReadOnlyCollection<Domain.Entities.Application>>
{
    public IReadOnlyCollection<Domain.Entities.Application> Execute(GetApplicationsRequest parameter)
    {
        return applicationRepository.GetAll();
    }
}