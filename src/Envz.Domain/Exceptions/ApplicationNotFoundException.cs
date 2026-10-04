namespace Envz.Domain.Exceptions;

public class ApplicationNotFoundException(string message, ExceptionCode code, string applicationName)
    : EnvzException(message, code, applicationName);