using Envz.Domain.Entities;

namespace Envz.Domain.Ports;

public interface IWorkspaceRepository
{
    void Save(Workspace workspace);

    IReadOnlyCollection<Workspace> GetAll();
    bool Exists(string workspaceName);

    void Update(IReadOnlyCollection<Workspace> workspaces);
}