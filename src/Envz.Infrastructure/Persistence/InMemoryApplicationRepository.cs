using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Infrastructure.Persistence;

public class InMemoryApplicationRepository : IApplicationRepository
{
    private readonly List<Application> _applications = [];

    public IReadOnlyCollection<Application> GetAll()
    {
        return _applications;
    }

    public void Save(Application application)
    {
        _applications.Add(application);
    }
}