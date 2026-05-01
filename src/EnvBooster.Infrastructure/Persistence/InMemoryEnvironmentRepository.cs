using EnvBooster.Domain.Ports;

namespace EnvBooster.Infrastructure.Persistence;

public class InMemoryEnvironmentRepository : IEnvironmentRepository
{
    private readonly List<Environment> _environments = [];

    public IReadOnlyCollection<Environment> GetAll()
    {
        return _environments;
    }

    public void Save(string name)
    {
        _environments.Add(new Environment(Guid.NewGuid().ToString(), name));
    }
}