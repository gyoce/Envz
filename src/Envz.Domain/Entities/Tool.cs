using Envz.Domain.Exceptions;

namespace Envz.Domain.Entities;

public class Tool
{
    public string Name { get; }
    public string Path { get; }
    public byte[]? Icon { get; }

    public Tool(string name, string path, byte[]? icon = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Tool name must not be empty", ExceptionCode.ToolInvalidName);

        if (string.IsNullOrWhiteSpace(path))
            throw new ValidationException("Tool path must not be empty", ExceptionCode.ToolInvalidPath);

        Name = name;
        Path = path;
        Icon = icon;
    }
}