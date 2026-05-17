using Envz.Domain.Ports;

namespace Envz.Infrastructure.Persistence;

public class InMemoryEnvironmentRepository : IEnvironmentRepository
{
    private readonly List<Environment> _environments = [];
    private int _environmentIdCounter;

    public IReadOnlyCollection<Environment> GetAll()
    {
        return _environments;
    }

    public void Save(Environment environment)
    {
        environment.Id = _environmentIdCounter++;
        _environments.Add(environment);
    }
}