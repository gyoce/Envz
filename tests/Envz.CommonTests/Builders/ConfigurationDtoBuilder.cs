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

    public ConfigurationDtoBuilder WithWorkspace(WorkspaceDto workspace)
    {
        _configuration.Workspaces.Add(workspace);
        return this;
    }

    public ConfigurationDto Build()
    {
        return _configuration;
    }

    public static ConfigurationDto EmptyConfiguration()
    {
        return new ConfigurationDto();
    }
}