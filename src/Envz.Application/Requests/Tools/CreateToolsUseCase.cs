using Envz.Application.Factories;
using Envz.Application.Mediator;
using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Domain.Validation;
using System.Runtime.ExceptionServices;

namespace Envz.Application.Requests.Tools;

public record ToolToCreate(string Name, string Path, byte[]? Icon = null);

public record CreateToolsRequest : IRequest
{
    public required IReadOnlyCollection<ToolToCreate> Tools { get; init; }
}

public class CreateToolsUseCase(IToolRepository toolRepository, ToolFactory toolFactory) : IUseCase<CreateToolsRequest>
{
    public void Execute(CreateToolsRequest request)
    {
        List<Tool> tools = [];
        foreach (ToolToCreate toolToCreate in request.Tools)
        {
            ValidationResult<Tool> result = toolFactory.Create(toolToCreate.Name, toolToCreate.Path, toolToCreate.Icon);
            if (!result.IsValid)
                ExceptionDispatchInfo.Throw(result.Error);
            tools.Add(result.Value);
        }

        toolRepository.Save(tools);
    }
}
