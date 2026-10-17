namespace Envz.Domain.Exceptions.Tools;

public class ToolInUseException(string message, ExceptionCode code, string toolName)
    : EnvzException(message, code, toolName);
