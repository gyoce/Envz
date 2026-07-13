using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration;

namespace Envz.Infrastructure.Persistence;

public class EnvironmentRepository(IConfigurationStore configurationStore) : IEnvironmentRepository
{
    public IReadOnlyCollection<Environment> GetAll()
    {
        return configurationStore.Configuration.Environments.Select(env => env.ToDomainEntity()).ToList();
    }

    public void Save(Environment environment)
    {
        configurationStore.Configuration.Environments.Add(environment.ToDto());
        configurationStore.Save();
    }
}
