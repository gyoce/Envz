using Envz.Application.Mediator;
using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Exceptions.Tools;
using Envz.Domain.Ports;

namespace Envz.Application.Requests.Tools;

public record CreateToolRequest : IRequest
{
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public byte[]? Icon { get; set; }
}

public class CreateToolUseCase(IToolRepository toolRepository) : IUseCase<CreateToolRequest>
{
    public void Execute(CreateToolRequest request)
    {
        if (toolRepository.Exists(request.Name))
        {
            throw new ToolAlreadyExistsException(
                $"Could not create tool with name `{request.Name}` because it already exists.",
                ExceptionCode.CreateToolAlreadyExists,
                request.Name
            );
        }

        toolRepository.Save(new Tool(request.Name, request.Path, request.Icon));
    }
}