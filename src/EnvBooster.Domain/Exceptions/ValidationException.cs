namespace EnvBooster.Domain.Exceptions;

public class ValidationException(string message) : Exception(message);