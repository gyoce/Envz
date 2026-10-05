namespace Envz.Domain.Exceptions.Applications;

public class ApplicationNotFoundException(string message, ExceptionCode code, string applicationName)
    : EnvzException(message, code, applicationName);