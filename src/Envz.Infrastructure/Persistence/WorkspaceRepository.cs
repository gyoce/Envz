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
        Workspaces.Add(
            new WorkspaceDto
            {
                Name = workspace.Name,
                Tools = workspace.Tools.Select(workspaceTool =>
                    new WorkspaceToolDto
                    {
                        ToolName = workspaceTool.ToolName,
                        Parameter = workspaceTool.Parameter
                    }
                ).ToList()
            }
        );
        configurationStore.Save();
    }

    public bool Exists(string workspaceName)
    {
        return Workspaces.Any(workspace => workspace.Name.EqualsIgnoringCase(workspaceName));
    }
}
