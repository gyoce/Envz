using Envz.Domain.Ports;

namespace Envz.Infrastructure.Persistence;

public class InMemoryEnvironmentRepository : IEnvironmentRepository
{
    private readonly List<Environment> _environments = [];

    public IReadOnlyCollection<Environment> GetAll()
    {
        return _environments;
    }

    public void Save(Environment environment)
    {
        _environments.Add(environment);
    }
}