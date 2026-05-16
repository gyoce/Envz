using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Functional.Mediator;

namespace Envz.Functional.Applications;

public record CreateApplicationRequest : IRequest
{
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public byte[] Icon { get; set; } = [];
}

public class CreateApplicationUseCase(IApplicationRepository applicationRepository) : IUseCase<CreateApplicationRequest>
{
    public void Execute(CreateApplicationRequest parameter)
    {
        applicationRepository.Save(new Application
        {
            Name = parameter.Name,
            Icon = parameter.Icon,
            Path = parameter.Path
        });
    }
}