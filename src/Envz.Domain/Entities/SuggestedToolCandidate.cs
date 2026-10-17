using Envz.Domain.Exceptions;

namespace Envz.Domain.Entities;

public class SuggestedToolCandidate
{
    public string Name { get; }
    public string Path { get; }

    public SuggestedToolCandidate(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("SuggestedToolCandidate name must not be empty", ExceptionCode.ToolInvalidName);

        if (string.IsNullOrWhiteSpace(path))
            throw new ValidationException("SuggestedToolCandidate path must not be empty", ExceptionCode.ToolInvalidPath);

        Name = name;
        Path = path;
    }
}