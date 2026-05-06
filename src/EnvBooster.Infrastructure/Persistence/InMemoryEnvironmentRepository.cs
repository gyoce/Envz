using EnvBooster.Domain.Ports;

namespace EnvBooster.Infrastructure.Persistence;

public class InMemoryEnvironmentRepository : IEnvironmentRepository
{
    private readonly List<Environment> _environments = [];

    public IReadOnlyCollection<Environment> GetAll()
    {
        return _environments;
    }

    public void Save(Environment environment)
    {
        environment.Id = Guid.NewGuid().ToString();
        _environments.Add(environment);
    }
}