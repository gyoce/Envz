namespace Envz.Domain.Exceptions.Tools;

public class ToolNotFoundException(string message, ErrorCode code, string toolName)
    : EnvzException(message, code, toolName);