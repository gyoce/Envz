namespace Envz.Domain.Exceptions;

public class ApplicationAlreadyExistsException(string message, ExceptionCode code, string applicationName)
    : EnvzException(message, code, applicationName);
