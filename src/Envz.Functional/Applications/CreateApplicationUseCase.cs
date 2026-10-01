using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Ports;
using Envz.Functional.Mediator;

namespace Envz.Functional.Applications;

public record CreateApplicationRequest : IRequest
{
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public byte[]? Icon { get; set; }
}

public class CreateApplicationUseCase(IApplicationRepository applicationRepository) : IUseCase<CreateApplicationRequest>
{
    public void Execute(CreateApplicationRequest request)
    {
        if (applicationRepository.Exists(request.Name))
        {
            throw new ApplicationAlreadyExistsException($"Application `{request.Name}` already exists.");
        }

        applicationRepository.Save(new Application
        {
            Name = request.Name,
            Icon = request.Icon,
            Path = request.Path
        });
    }
}