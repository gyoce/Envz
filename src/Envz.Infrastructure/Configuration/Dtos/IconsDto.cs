using System.Text.Json.Serialization;

namespace Envz.Infrastructure.Configuration.Dtos;

public class IconsDto
{
    [JsonPropertyName("applications")]
    [JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
    public Dictionary<string, string> ToolIcons { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}