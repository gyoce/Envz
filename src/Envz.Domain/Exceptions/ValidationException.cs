namespace Envz.Domain.Exceptions;

public class ValidationException(string message, ExceptionCode code, params object[] args)
    : EnvzException(message, code, args);