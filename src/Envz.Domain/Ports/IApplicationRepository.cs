using Envz.Domain.Entities;

namespace Envz.Domain.Ports;

public interface IApplicationRepository
{
    IReadOnlyCollection<Application> GetAll();
    void Save(Application application);
}