using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.CommonTests.Builders;

public class WorkspaceToolDtoBuilder
{
    private readonly WorkspaceToolDto _workspaceToolDto = new();

    public WorkspaceToolDtoBuilder WithToolName(string toolName)
    {
        _workspaceToolDto.ToolName = toolName;
        return this;
    }

    public WorkspaceToolDtoBuilder WithParameter(string? parameter)
    {
        _workspaceToolDto.Parameter = parameter;
        return this;
    }

    public WorkspaceToolDto Build()
    {
        return _workspaceToolDto;
    }
}