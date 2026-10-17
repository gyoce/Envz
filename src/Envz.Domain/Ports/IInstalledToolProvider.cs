using Envz.Domain.Entities;

namespace Envz.Domain.Ports;

public interface IInstalledToolProvider
{
    Task<IReadOnlyCollection<Tool>> GetInstalledAsync();
}