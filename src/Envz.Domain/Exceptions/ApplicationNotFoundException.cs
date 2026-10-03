namespace Envz.Domain.Exceptions;

public class ApplicationNotFoundException(string applicationName)
    : EnvzException($"Application `{applicationName}` not found.")
{
    public string ApplicationName { get; } = applicationName;
}