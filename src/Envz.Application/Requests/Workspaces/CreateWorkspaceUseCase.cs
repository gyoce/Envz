using Envz.Application.Mediator;
using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Exceptions.Tools;
using Envz.Domain.Exceptions.Workspaces;
using Envz.Domain.Ports;

namespace Envz.Application.Requests.Workspaces;

public record CreateWorkspaceRequest : IRequest
{
    public string Name { get; set; } = string.Empty;
    public List<WorkspaceTool> Tools { get; set; } = [];
}

public class CreateWorkspaceUseCase(IWorkspaceRepository workspaceRepository, IToolRepository toolRepository) : IUseCase<CreateWorkspaceRequest>
{
    public void Execute(CreateWorkspaceRequest request)
    {
        if (workspaceRepository.Exists(request.Name))
        {
            throw new WorkspaceAlreadyExistsException(
                $"Could not create workspace because Workspace `{request.Name}` already exists",
                ExceptionCode.CreateWorkspaceAlreadyExists,
                request.Name
            );
        }

        foreach (WorkspaceTool workspaceTool in request.Tools)
        {
            if (!toolRepository.Exists(workspaceTool.ToolName))
            {
                throw new ToolNotFoundException(
                    $"Could not create workspace because tool `{workspaceTool.ToolName}` was not found.",
                    ExceptionCode.CreateWorkspaceToolNotFound,
                    workspaceTool.ToolName
                );
            }
        }

        workspaceRepository.Save(new Workspace(request.Name, request.Tools));
    }
}