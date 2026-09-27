using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.Infrastructure.Configuration.Stores;

public interface IConfigurationStore
{
    ConfigurationDto Configuration { get; }

    void Save();
}

public class ConfigurationStore(IFileSystem fileSystem, IConfigurationFilesPathProvider filesPathProvider)
    : JsonFileStore<ConfigurationDto>(fileSystem), IConfigurationStore
{
    protected override string FilePath => filesPathProvider.ConfigurationFilePath;
    public ConfigurationDto Configuration => Data;
}
