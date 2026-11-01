using Envz.Application.Mediator;
using Envz.Application.Validators;
using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Exceptions.Tools;
using Envz.Domain.Ports;
using Envz.Domain.Validation;

namespace Envz.Application.Requests.Tools;

public record EditToolRequest : IRequest
{
    public required string ToolName { get; init; }
    public required string NewName { get; init; }
    public required string NewPath { get; init; }
    public byte[]? NewIcon { get; init; }
}

public class EditToolUseCase(IToolRepository toolRepository, IWorkspaceRepository workspaceRepository, ToolValidator toolValidator) : IUseCase<EditToolRequest>
{
    public void Execute(EditToolRequest request)
    {
        Tool original = toolRepository.Get(request.ToolName) 
            ?? throw new ToolNotFoundException(
                $"Could not edit tool `{request.ToolName}` because it was not found.",
                ErrorCode.EditToolNotFound,
                request.ToolName
            );

        ValidationResult<Tool> result = toolValidator.ValidateEdition(original, request.NewName, request.NewPath, request.NewIcon);
        Tool newTool = result.GetValueOrThrow();

        List<Workspace> impactedWorkspaces = newTool.Name == original.Name
            ? []
            : workspaceRepository.GetAll()
                .Where(w => w.Uses(original.Name))
                .Select(w => w.WithRenamedTool(original.Name, newTool.Name))
                .ToList();

        toolRepository.Update(original.Name, newTool);
        
        if (impactedWorkspaces.Any())
            workspaceRepository.Update(impactedWorkspaces);
    }
}