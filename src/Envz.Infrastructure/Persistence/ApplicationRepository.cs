using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration;

namespace Envz.Infrastructure.Persistence;

public class ApplicationRepository(IConfigurationStore configurationStore) : IApplicationRepository
{
    public IReadOnlyCollection<Application> GetAll()
    {
        return configurationStore.Configuration.Applications.Select(app => app.ToDomainEntity()).ToList();
    }

    public void Save(Application application)
    {
        configurationStore.Configuration.Applications.Add(application.ToDto());
        configurationStore.Save();
    }
}
