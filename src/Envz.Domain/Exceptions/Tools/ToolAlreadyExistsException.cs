namespace Envz.Domain.Exceptions.Tools;

public class ToolAlreadyExistsException(string message, ErrorCode code, string toolName)
    : ValidationException(message, code, toolName);
