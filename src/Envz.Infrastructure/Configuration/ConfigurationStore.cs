using System.Text.Json;

namespace Envz.Infrastructure.Configuration;

public class ConfigurationStore(IFileSystem fileSystem, IConfigurationPathProvider pathProvider) : IConfigurationStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new() { WriteIndented = true, IndentSize = 2 };

    public ConfigurationDto Configuration
    {
        get => field ??= LoadConfiguration();
    }

    public void Save()
    {
        string path = pathProvider.ConfigurationFilePath;
        string? directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
            fileSystem.CreateDirectory(directory);

        string json = JsonSerializer.Serialize(Configuration, SerializerOptions);
        fileSystem.WriteAllText(path, json);
    }

    private ConfigurationDto LoadConfiguration()
    {
        string path = pathProvider.ConfigurationFilePath;
        if (!fileSystem.Exists(path))
            return new ConfigurationDto();

        try
        {
            string json = fileSystem.ReadAllText(path);
            return JsonSerializer.Deserialize<ConfigurationDto>(json, SerializerOptions) ?? new ConfigurationDto();
        }
        catch (JsonException)
        {
            return new ConfigurationDto();
        }
    }
}
