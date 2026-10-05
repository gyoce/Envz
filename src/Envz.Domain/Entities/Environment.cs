using Envz.Domain.Exceptions;

namespace Envz.Domain.Entities;

public class Environment
{
    public string Name { get; }
    public IReadOnlyList<EnvironmentApplication> Applications { get; }

    public Environment(string name, IEnumerable<EnvironmentApplication> applications)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Environment name must not be empty.", ExceptionCode.EnvironmentInvalidName);

        Name = name;
        Applications = applications.ToList();
    }
}