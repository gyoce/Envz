using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.CommonTests.Builders;

public class ConfigurationDtoBuilder
{
    private readonly ConfigurationDto _configuration = new();

    public ConfigurationDtoBuilder WithTool(ToolDto tool)
    {
        _configuration.Tools.Add(tool);
        return this;
    }

    public ConfigurationDtoBuilder WithTools(params ToolDto[] tools)
    {
        foreach (ToolDto tool in tools)
            WithTool(tool);
        return this;
    }

    public ConfigurationDtoBuilder WithWorkspace(WorkspaceDto workspace)
    {
        _configuration.Workspaces.Add(workspace);
        return this;
    }

    public ConfigurationDtoBuilder WithWorkspaces(params WorkspaceDto[] workspaces)
    {
        foreach (WorkspaceDto workspace in workspaces)
            WithWorkspace(workspace);
        return this;
    }

    public ConfigurationDtoBuilder WithIcons(IconsDto icons)
    {
        _configuration.Icons = icons;
        return this;
    }

    public ConfigurationDto Build()
    {
        return _configuration;
    }

    public static implicit operator ConfigurationDto(ConfigurationDtoBuilder builder) => builder.Build();

    public static ConfigurationDto EmptyConfiguration()
    {
        return new ConfigurationDto();
    }
}