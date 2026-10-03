using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.Infrastructure.Persistence;

public class SettingsRepository(IConfigurationStore configurationStore) : ISettingsRepository
{
    public UserSettings Get()
    {
        return new UserSettings { Language = configurationStore.Configuration.Settings.Language };
    }

    public void Save(UserSettings settings)
    {
        configurationStore.Configuration.Settings.Language = settings.Language;
        configurationStore.Save();
    }
}