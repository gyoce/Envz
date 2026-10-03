using Envz.Domain.Exceptions;
using Envz.Domain.Ports;
using Envz.Functional.Mediator;

namespace Envz.Functional.Applications;

public record DeleteApplicationRequest : IRequest
{
    public required string ApplicationName { get; set; } = string.Empty;
}

public class DeleteApplicationUseCase(IApplicationRepository applicationRepository, IEnvironmentRepository environmentRepository) : IUseCase<DeleteApplicationRequest>
{
    public void Execute(DeleteApplicationRequest request)
    {
        if (!applicationRepository.Exists(request.ApplicationName))
        {
            throw new ApplicationNotFoundException(request.ApplicationName);
        }

        IReadOnlyCollection<Environment> environments = environmentRepository.GetAll();
        if (environments.Any(env => env.Applications.Any(app => app.ApplicationName == request.ApplicationName)))
        {
            throw new ApplicationInUseException(request.ApplicationName);
        }

        applicationRepository.Delete(request.ApplicationName);
    }
}