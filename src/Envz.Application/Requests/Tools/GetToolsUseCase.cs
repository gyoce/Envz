using Envz.Application.Mediator;
using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Application.Requests.Tools;

public record GetToolsRequest : IRequest<IReadOnlyCollection<Tool>>;

public class GetToolsUseCase(IToolRepository toolRepository) : IUseCase<GetToolsRequest, IReadOnlyCollection<Tool>>
{
    public IReadOnlyCollection<Tool> Execute(GetToolsRequest request)
    {
        return toolRepository.GetAll();
    }
}