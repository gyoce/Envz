using Envz.Domain.Exceptions;

namespace Envz.Domain.Entities;

public class Application
{
    public string Name { get; }
    public string Path { get; }
    public byte[]? Icon { get; }

    public Application(string name, string path, byte[]? icon = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Application name must not be empty", ExceptionCode.ApplicationInvalidName);

        if (string.IsNullOrWhiteSpace(path))
            throw new ValidationException("Application path must not be empty", ExceptionCode.ApplicationInvalidPath);

        Name = name;
        Path = path;
        Icon = icon;
    }
}