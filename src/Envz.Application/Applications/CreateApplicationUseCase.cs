using Envz.Application.Mediator;

namespace Envz.Application.Applications;

public record CreateApplicationRequest : IRequest
{
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public byte[] Icon { get; set; } = [];
}

public class CreateApplicationUseCase : IUseCase<CreateApplicationRequest>
{
    public void Execute(CreateApplicationRequest parameter)
    {

    }
}