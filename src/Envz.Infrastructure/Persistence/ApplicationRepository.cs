using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.Infrastructure.Persistence;

public class ApplicationRepository(IConfigurationStore configurationStore, IIconStore iconStore) : IApplicationRepository
{
    public IReadOnlyCollection<Application> GetAll()
    {
        Dictionary<string, string> icons = iconStore.Icons.ApplicationIcons;

        return configurationStore.Configuration.Applications.Select(applicationDto =>
            new Application
            {
                Name = applicationDto.Name,
                Path = applicationDto.Path,
                Icon = icons.TryGetValue(applicationDto.Name, out string? icon) && !string.IsNullOrWhiteSpace(icon)
                    ? Convert.FromBase64String(icon)
                    : null
            }
        ).ToList();
    }

    public void Save(Application application)
    {
        configurationStore.Configuration.Applications.Add(
            new ApplicationDto
            {
                Name = application.Name,
                Path = application.Path
            }
        );
        configurationStore.Save();

        if (application.Icon is not null && application.Icon.Length > 0)
        {
            iconStore.Icons.ApplicationIcons[application.Name] = Convert.ToBase64String(application.Icon);
            iconStore.Save();
        }
    }

    public bool Exists(string applicationName)
    {
        return configurationStore.Configuration.Applications.Any(app => app.Name == applicationName);
    }

    public void Delete(string applicationName)
    {
        configurationStore.Configuration.Applications.RemoveAll(app => app.Name == applicationName);
        configurationStore.Save();
        iconStore.Icons.ApplicationIcons.Remove(applicationName);
        iconStore.Save();
    }
}
