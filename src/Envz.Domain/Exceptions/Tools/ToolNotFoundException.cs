namespace Envz.Domain.Exceptions.Tools;

public class ToolNotFoundException(string message, ExceptionCode code, string toolName)
    : EnvzException(message, code, toolName);