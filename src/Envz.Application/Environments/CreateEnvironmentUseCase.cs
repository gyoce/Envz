using Envz.Application.Mediator;
using Envz.Domain.Exceptions;
using Envz.Domain.Ports;

namespace Envz.Application.Environments;

public record CreateEnvironmentRequest : IRequest
{
    public string Name { get; set; } = string.Empty;
}

public class CreateEnvironmentUseCase(IEnvironmentRepository environmentRepository) : IUseCase<CreateEnvironmentRequest>
{
    public void Execute(CreateEnvironmentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException($"{nameof(Environment.Name)} must not be null or white space.");

        environmentRepository.Save(new Environment
        {
            Name = request.Name
        });
    }
}