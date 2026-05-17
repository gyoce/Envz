using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Infrastructure.Persistence;

public class InMemoryApplicationRepository : IApplicationRepository
{
    private readonly List<Application> _applications = [];
    private int _applicationIdCounter;

    public IReadOnlyCollection<Application> GetAll()
    {
        return _applications;
    }

    public void Save(Application application)
    {
        application.Id = _applicationIdCounter++;
        _applications.Add(application);
    }
}