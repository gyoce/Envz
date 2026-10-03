namespace Envz.Domain.Exceptions;

public class ApplicationAlreadyExistsException(string applicationName)
    : EnvzException($"Application `{applicationName}` already exists.")
{
    public string ApplicationName { get; } = applicationName;
}
