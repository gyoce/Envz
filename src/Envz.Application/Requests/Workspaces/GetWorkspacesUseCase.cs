using Envz.Application.Mediator;
using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Application.Requests.Workspaces;

public record GetWorkspacesRequest : IRequest<IReadOnlyCollection<Workspace>>;

public class GetWorkspacesUseCase(IWorkspaceRepository workspaceRepository) : IUseCase<GetWorkspacesRequest, IReadOnlyCollection<Workspace>>
{
    public IReadOnlyCollection<Workspace> Execute(GetWorkspacesRequest request)
    {
        return workspaceRepository.GetAll();
    }
}