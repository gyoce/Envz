using Envz.Domain.Exceptions;

namespace Envz.Domain.Entities;

public class EnvironmentApplication
{
    public string ApplicationName { get; }
    public string? Parameter { get; }

    public EnvironmentApplication(string applicationName, string? parameter = null)
    {
        if (string.IsNullOrWhiteSpace(applicationName))
            throw new ValidationException("Application name must not be empty", ExceptionCode.ApplicationInvalidName);

        ApplicationName = applicationName;
        Parameter = parameter;
    }
}