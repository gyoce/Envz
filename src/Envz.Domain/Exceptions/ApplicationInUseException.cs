namespace Envz.Domain.Exceptions;

public class ApplicationInUseException(string message) : EnvzException(message);
