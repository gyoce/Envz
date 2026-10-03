namespace Envz.Domain.Exceptions;

public class ApplicationInUseException(string applicationName)
    : EnvzException($"Application `{applicationName}` is still in use.")
{
    public string ApplicationName { get; } = applicationName;
}
