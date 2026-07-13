namespace Envz.Infrastructure.Configuration;

public interface IConfigurationPathProvider
{
    string ConfigurationFilePath { get; }
}

public class ConfigurationPathProvider : IConfigurationPathProvider
{
    public string ConfigurationFilePath { get; } = Path.Combine(
        System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
        "Envz",
        "configuration.json");
}
