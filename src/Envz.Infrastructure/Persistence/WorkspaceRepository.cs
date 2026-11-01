using Envz.Domain.Entities;
using Envz.Domain.Extensions;
using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.Infrastructure.Persistence;

public class WorkspaceRepository(IConfigurationStore configurationStore) : IWorkspaceRepository
{
    private List<WorkspaceDto> Workspaces => configurationStore.Configuration.Workspaces;

    public IReadOnlyCollection<Workspace> GetAll()
    {
        return Workspaces.Select(workspaceDto => workspaceDto.ToEntity()).ToList();
    }

    public void Save(Workspace workspace)
    {
        Workspaces.Add(workspace.ToDto());
        configurationStore.Save();
    }

    public bool Exists(string workspaceName)
    {
        return Workspaces.Any(workspace => workspace.Name.EqualsIgnoringCase(workspaceName));
    }

    public void Update(IReadOnlyCollection<Workspace> workspaces)
    {
        foreach (Workspace workspace in workspaces)
        {
            int index = Workspaces.FindIndex(w => w.Name.EqualsIgnoringCase(workspace.Name));
            Workspaces[index] = workspace.ToDto();
        }
        configurationStore.Save();
    }
}
