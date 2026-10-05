using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.Infrastructure.Persistence;

public class EnvironmentRepository(IConfigurationStore configurationStore) : IEnvironmentRepository
{
    private List<EnvironmentDto> Environments => configurationStore.Configuration.Environments;

    public IReadOnlyCollection<Environment> GetAll()
    {
        return Environments.Select(environmentDto => environmentDto.ToEntity()).ToList();
    }

    public void Save(Environment environment)
    {
        Environments.Add(
            new EnvironmentDto
            {
                Name = environment.Name,
                Applications = environment.Applications.Select(envApp =>
                    new EnvironmentApplicationDto
                    {
                        ApplicationName = envApp.ApplicationName,
                        Parameter = envApp.Parameter
                    }
                ).ToList()
            }
        );
        configurationStore.Save();
    }

    public bool Exists(string environmentName)
    {
        return Environments.Any(env => env.Name == environmentName);
    }
}
