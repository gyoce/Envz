namespace Envz.Domain.Exceptions.Applications;

public class ApplicationInUseException(string message, ExceptionCode code, string applicationName)
    : EnvzException(message, code, applicationName);
