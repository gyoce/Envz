using Envz.Domain.Entities;

namespace Envz.Domain.Ports;

public interface IWorkspaceRepository
{
    IReadOnlyCollection<Workspace> GetAll();
    void Save(Workspace workspace);
    bool Exists(string workspaceName);
}