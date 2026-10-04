namespace Envz.Domain.Exceptions;

public class ApplicationInUseException(string message, ExceptionCode code, string applicationName)
    : EnvzException(message, code, applicationName);
