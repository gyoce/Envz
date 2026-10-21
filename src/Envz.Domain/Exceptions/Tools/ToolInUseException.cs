namespace Envz.Domain.Exceptions.Tools;

public class ToolInUseException(string message, ErrorCode code, string toolName)
    : EnvzException(message, code, toolName);
