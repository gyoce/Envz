using Envz.Domain.Exceptions;

namespace Envz.Domain.Entities;

public class SuggestedApplicationCandidate
{
    public string Name { get; }
    public string Path { get; }

    public SuggestedApplicationCandidate(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("SuggestedApplicationCandidate name must not be empty", ExceptionCode.ApplicationInvalidName);

        if (string.IsNullOrWhiteSpace(path))
            throw new ValidationException("SuggestedApplicationCandidate path must not be empty", ExceptionCode.ApplicationInvalidPath);

        Name = name;
        Path = path;
    }
}