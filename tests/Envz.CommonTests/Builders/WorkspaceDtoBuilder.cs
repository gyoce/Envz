using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.CommonTests.Builders;

public class WorkspaceDtoBuilder
{
    private readonly WorkspaceDto _workspaceDto = new();

    public WorkspaceDtoBuilder WithName(string name)
    {
        _workspaceDto.Name = name;
        return this;
    }

    public WorkspaceDtoBuilder WithTool(WorkspaceToolDto tool)
    {
        _workspaceDto.Tools.Add(tool);
        return this;
    }

    public WorkspaceDto Build()
    {
        return _workspaceDto;
    }
}