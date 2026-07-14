using System.Text.Json.Serialization;

namespace Envz.Infrastructure.Configuration;

public class ConfigurationDto
{
    [JsonPropertyName("environments")]
    public List<EnvironmentDto> Environments { get; set; } = [];

    [JsonPropertyName("applications")]
    public List<ApplicationDto> Applications { get; set; } = [];
}

public class EnvironmentDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("applications")]
    public List<EnvironmentApplicationDto> Applications { get; set; } = [];
}

public class ApplicationDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }
}

public class EnvironmentApplicationDto
{
    [JsonPropertyName("applicationName")]
    public string ApplicationName { get; set; } = string.Empty;

    [JsonPropertyName("parameter")]
    public string? Parameter { get; set; }
}