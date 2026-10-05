namespace Envz.Domain.Exceptions.Applications;

public class ApplicationAlreadyExistsException(string message, ExceptionCode code, string applicationName)
    : EnvzException(message, code, applicationName);
