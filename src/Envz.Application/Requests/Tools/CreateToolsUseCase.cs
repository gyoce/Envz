using Envz.Application.Mediator;
using Envz.Application.Validators;
using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Domain.Validation;

namespace Envz.Application.Requests.Tools;

public record ToolToCreate(string Name, string Path, byte[]? Icon = null);

public record CreateToolsRequest : IRequest
{
    public required IReadOnlyCollection<ToolToCreate> Tools { get; init; }
}

public class CreateToolsUseCase(IToolRepository toolRepository, ToolValidator toolValidator) : IUseCase<CreateToolsRequest>
{
    public void Execute(CreateToolsRequest request)
    {
        List<Tool> tools = [];
        foreach (ToolToCreate toolToCreate in request.Tools)
        {
            ValidationResult<Tool> result = toolValidator.ValidateCreation(toolToCreate.Name, toolToCreate.Path, toolToCreate.Icon);
            tools.Add(result.GetValueOrThrow());
        }

        toolRepository.Save(tools);
    }
}
