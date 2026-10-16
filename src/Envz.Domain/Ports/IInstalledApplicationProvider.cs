using Envz.Domain.Entities;

namespace Envz.Domain.Ports;

public interface IInstalledApplicationProvider
{
    Task<IReadOnlyCollection<Application>> GetInstalledAsync();
}