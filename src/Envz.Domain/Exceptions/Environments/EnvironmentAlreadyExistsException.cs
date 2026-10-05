namespace Envz.Domain.Exceptions.Environments;

public class EnvironmentAlreadyExistsException(string message, ExceptionCode code, string environmentName) : EnvzException(message, code, environmentName);
