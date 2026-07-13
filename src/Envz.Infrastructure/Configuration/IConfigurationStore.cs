namespace Envz.Infrastructure.Configuration;

public interface IConfigurationStore
{
    ConfigurationDto Configuration { get; }

    void Save();
}