namespace Envz.Domain.Exceptions;

public class ValidationException(string message, ErrorCode code, params object[] args)
    : EnvzException(message, code, args);