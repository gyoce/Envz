using Envz.Application.Mediator;
using Envz.Domain.Ports;

namespace Envz.Application.Environments;

public record GetEnvironmentsRequest : IRequest<IReadOnlyCollection<Environment>>;

public class GetEnvironmentsUseCase(IEnvironmentRepository environmentRepository) : IUseCase<GetEnvironmentsRequest, IReadOnlyCollection<Environment>>
{
    public IReadOnlyCollection<Environment> Execute(GetEnvironmentsRequest request)
    {
        return environmentRepository.GetAll();
    }
}