using Envz.Domain.Exceptions;

namespace Envz.Domain.Entities;

public class WorkspaceTool
{
    public string ToolName { get; }
    public string? Parameter { get; }

    public WorkspaceTool(string toolName, string? parameter = null)
    {
        if (string.IsNullOrWhiteSpace(toolName))
            throw new ValidationException("Tool name must not be empty", ExceptionCode.ToolInvalidName);

        ToolName = toolName;
        Parameter = parameter;
    }
}