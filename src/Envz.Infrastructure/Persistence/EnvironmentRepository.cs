using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration;

namespace Envz.Infrastructure.Persistence;

public class EnvironmentRepository(IConfigurationStore configurationStore) : IEnvironmentRepository
{
    public IReadOnlyCollection<Environment> GetAll()
    {
        return configurationStore.Configuration.Environments.Select(
            environmentDto => new Environment
            {
                Name = environmentDto.Name,
                Applications = environmentDto.Applications.Select(envAppDto =>
                    new EnvironmentApplication
                    {
                        ApplicationName = envAppDto.ApplicationName,
                        Parameter = envAppDto.Parameter
                    }
                ).ToList()
            }
        ).ToList();
    }

    public void Save(Environment environment)
    {
        configurationStore.Configuration.Environments.Add(
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
}
