using Envz.Domain.Exceptions;
using Envz.Domain.Exceptions.Applications;
using Envz.Domain.Ports;
using Envz.Functional.Mediator;

namespace Envz.Functional.Applications;

public record DeleteApplicationRequest : IRequest
{
    public required string ApplicationName { get; set; }
}

public class DeleteApplicationUseCase(IApplicationRepository applicationRepository, IEnvironmentRepository environmentRepository) : IUseCase<DeleteApplicationRequest>
{
    public void Execute(DeleteApplicationRequest request)
    {
        if (!applicationRepository.Exists(request.ApplicationName))
        {
            throw new ApplicationNotFoundException(
                $"Could not delete application `{request.ApplicationName}` because it was not found.",
                ExceptionCode.DeleteApplicationNotFound,
                request.ApplicationName
            );
        }

        IReadOnlyCollection<Environment> environments = environmentRepository.GetAll();
        if (environments.Any(env => env.Applications.Any(app => app.ApplicationName == request.ApplicationName)))
        {
            throw new ApplicationInUseException(
                $"Could not delete application `{request.ApplicationName}` because it is still in use.",
                ExceptionCode.DeleteApplicationStillUsed,
                request.ApplicationName
            );
        }

        applicationRepository.Delete(request.ApplicationName);
    }
}