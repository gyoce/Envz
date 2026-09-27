using System.Text.Json.Serialization;

namespace Envz.Infrastructure.Configuration.Dtos;

public class IconsDto
{
    [JsonPropertyName("applications")]
    public Dictionary<string, string> ApplicationIcons { get; set; } = [];
}