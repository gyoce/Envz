using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration;

namespace Envz.Infrastructure.Persistence;

public class ApplicationRepository(IConfigurationStore configurationStore, IFileSystem fileSystem) : IApplicationRepository
{
    public IReadOnlyCollection<Application> GetAll()
    {
        return configurationStore.Configuration.Applications.Select(applicationDto =>
            new Application
            {
                Name = applicationDto.Name,
                Path = applicationDto.Path,
                Icon = string.IsNullOrWhiteSpace(applicationDto.Icon) ? null : Convert.FromBase64String(applicationDto.Icon)
            }
        ).ToList();
    }

    public void Save(Application application)
    {
        configurationStore.Configuration.Applications.Add(
            new ApplicationDto
            {
                Name = application.Name,
                Path = application.Path,
                Icon = application.Icon is null ? null : Convert.ToBase64String(application.Icon)
            }
        );
        configurationStore.Save();
    }

    public bool Exists(string applicationName)
    {
        return configurationStore.Configuration.Applications.Any(app => app.Name == applicationName);
    }
}
