using System.Text.Json.Serialization;

namespace Envz.Infrastructure.Configuration.Dtos;

public class ConfigurationDto
{
    [JsonPropertyName("environments")]
    public List<WorkspaceDto> Workspaces { get; set; } = [];

    [JsonPropertyName("applications")]
    public List<ToolDto> Tools { get; set; } = [];

    [JsonPropertyName("settings")]
    public SettingsDto Settings { get; set; } = new();
}

public class WorkspaceDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("applications")]
    public List<WorkspaceToolDto> Tools { get; set; } = [];
}

public class ToolDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;
}

public class WorkspaceToolDto
{
    [JsonPropertyName("applicationName")]
    public string ToolName { get; set; } = string.Empty;

    [JsonPropertyName("parameter")]
    public string? Parameter { get; set; }
}

public class SettingsDto
{
    [JsonPropertyName("language")]
    public string Language { get; set; } = "en";
}