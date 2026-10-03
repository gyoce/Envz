using Envz.Domain.Entities;

namespace Envz.Domain.Ports;

public interface ISettingsRepository
{
    UserSettings Get();
    void Save(UserSettings settings);
}