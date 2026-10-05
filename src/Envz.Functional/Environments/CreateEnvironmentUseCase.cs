using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Exceptions.Applications;
using Envz.Domain.Exceptions.Environments;
using Envz.Domain.Ports;
using Envz.Functional.Mediator;

namespace Envz.Functional.Environments;

public record CreateEnvironmentRequest : IRequest
{
    public string Name { get; set; } = string.Empty;
    public List<EnvironmentApplication> Applications { get; set; } = [];
}

public class CreateEnvironmentUseCase(IEnvironmentRepository environmentRepository, IApplicationRepository applicationRepository) : IUseCase<CreateEnvironmentRequest>
{
    public void Execute(CreateEnvironmentRequest request)
    {
        if (environmentRepository.Exists(request.Name))
        {
            throw new EnvironmentAlreadyExistsException(
                $"Could not create environment because environment `{request.Name}` already exists",
                ExceptionCode.CreateEnvironmentAlreadyExists,
                request.Name
            );
        }

        foreach (EnvironmentApplication envApp in request.Applications)
        {
            if (!applicationRepository.Exists(envApp.ApplicationName))
            {
                throw new ApplicationNotFoundException(
                    $"Could not create environment because application `{envApp.ApplicationName}` was not found.",
                    ExceptionCode.CreateEnvironmentApplicationNotFound,
                    envApp.ApplicationName
                );
            }
        }

        environmentRepository.Save(new Environment(request.Name, request.Applications));
    }
}