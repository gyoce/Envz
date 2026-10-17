using Envz.Application.Mediator;
using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Exceptions.Tools;
using Envz.Domain.Extensions;
using Envz.Domain.Ports;

namespace Envz.Application.Requests.Tools;

public record DeleteToolRequest : IRequest
{
    public required string ToolName { get; set; }
}

public class DeleteToolUseCase(IToolRepository toolRepository, IWorkspaceRepository workspaceRepository) : IUseCase<DeleteToolRequest>
{
    public void Execute(DeleteToolRequest request)
    {
        if (!toolRepository.Exists(request.ToolName))
        {
            throw new ToolNotFoundException(
                $"Could not delete tool `{request.ToolName}` because it was not found.",
                ExceptionCode.DeleteToolNotFound,
                request.ToolName
            );
        }

        IReadOnlyCollection<Workspace> workspaces = workspaceRepository.GetAll();
        if (workspaces.Any(env => env.Tools.Any(app => app.ToolName.EqualsIgnoringCase(request.ToolName))))
        {
            throw new ToolInUseException(
                $"Could not delete tool `{request.ToolName}` because it is still in use.",
                ExceptionCode.DeleteToolStillUsed,
                request.ToolName
            );
        }

        toolRepository.Delete(request.ToolName);
    }
}