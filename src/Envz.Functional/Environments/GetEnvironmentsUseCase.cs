using Envz.Domain.Ports;
using Envz.Functional.Mediator;

namespace Envz.Functional.Environments;

public record GetEnvironmentsRequest : IRequest<IReadOnlyCollection<Environment>>;

public class GetEnvironmentsUseCase(IEnvironmentRepository environmentRepository) : IUseCase<GetEnvironmentsRequest, IReadOnlyCollection<Environment>>
{
    public IReadOnlyCollection<Environment> Execute(GetEnvironmentsRequest request)
    {
        return environmentRepository.GetAll();
    }
}