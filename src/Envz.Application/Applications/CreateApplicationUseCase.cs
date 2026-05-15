using Envz.Application.Mediator;

namespace Envz.Application.Applications;

public record CreateApplicationRequest : IRequest
{
    public string AppicationPath { get; set; } = string.Empty;
}

public class CreateApplicationUseCase : IUseCase<CreateApplicationRequest>
{
    public void Execute(CreateApplicationRequest parameter)
    {
        
    }
}