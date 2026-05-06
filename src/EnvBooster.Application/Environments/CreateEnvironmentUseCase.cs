using EnvBooster.Domain.Exceptions;
using EnvBooster.Domain.Ports;

namespace EnvBooster.Application.Environments;

public record CreateEnvironmentRequest
{
    public string Name { get; set; } = string.Empty;
}

public class CreateEnvironmentUseCase(IEnvironmentRepository environmentRepository)
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