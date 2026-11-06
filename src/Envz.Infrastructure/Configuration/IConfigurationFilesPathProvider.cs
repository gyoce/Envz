using Envz.Domain;

namespace Envz.Infrastructure.Configuration;

public interface IConfigurationFilesPathProvider
{
    string ConfigurationFilePath { get; }
}

public class ConfigurationFilesPathProvider : IConfigurationFilesPathProvider
{
    private static readonly string BASE_FILE_PATH = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        Constants.PROJECT_NAME
    );

    public string ConfigurationFilePath { get; } = Path.Combine(BASE_FILE_PATH, "configuration.json");
}
