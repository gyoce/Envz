namespace Envz.Domain.Exceptions.Tools;

public class ToolAlreadyExistsException(string message, ExceptionCode code, string toolName)
    : EnvzException(message, code, toolName);
