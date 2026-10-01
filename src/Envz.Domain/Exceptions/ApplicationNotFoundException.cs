namespace Envz.Domain.Exceptions;

public class ApplicationNotFoundException(string message) : EnvzException(message);