using Envz.Infrastructure.Configuration;

namespace Envz.FunctionalTests.Configuration;

public class ConfigurationStoreTests : BaseTestFixture
{
    private const string Path = @"C:\Envz\configuration.json";

    public ConfigurationStoreTests()
    {
        ReplaceService<IFileSystem, InMemoryFileSystem>();
        ReplaceByMock<IConfigurationPathProvider>();

        GetMock<IConfigurationPathProvider>().Setup(p => p.ConfigurationFilePath).Returns(Path);
    }

    [Fact]
    public void ShouldReturnEmptyConfigurationWhenFileDoesNotExist()
    {
        IConfigurationStore store = GetService<IConfigurationStore>();

        store.Configuration.Applications.ShouldBeEmpty();
        store.Configuration.Environments.ShouldBeEmpty();
    }

    [Fact]
    public void ShouldLoadConfigurationFromFile()
    {
        GetServiceAs<IFileSystem, InMemoryFileSystem>().Files[Path] = """{ "applications": [ { "name": "App", "path": "C:\\app.exe" } ] }""";

        IConfigurationStore store = GetService<IConfigurationStore>();

        store.Configuration.Applications.Count.ShouldBe(1);
        store.Configuration.Applications[0].Name.ShouldBe("App");
    }

    [Fact]
    public void ShouldReturnEmptyConfigurationWhenFileIsCorrupted()
    {
        GetServiceAs<IFileSystem, InMemoryFileSystem>().Files[Path] = "{ this is not json";

        IConfigurationStore store = GetService<IConfigurationStore>();

        store.Configuration.Applications.ShouldBeEmpty();
    }

    [Fact]
    public void ShouldWriteConfigurationToFile()
    {
        IConfigurationStore store = GetService<IConfigurationStore>();
        store.Configuration.Applications.Add(new ApplicationDto { Name = "App", Path = "C:\\app.exe" });

        store.Save();

        InMemoryFileSystem fileSystem = GetServiceAs<IFileSystem, InMemoryFileSystem>();
        fileSystem.Files.ShouldContainKey(Path);
        fileSystem.Files[Path].ShouldContain("\"name\": \"App\"");
    }

    [Fact]
    public void ShouldLoadFileOnlyOnce()
    {
        InMemoryFileSystem fileSystem = GetServiceAs<IFileSystem, InMemoryFileSystem>();

        IConfigurationStore store = GetService<IConfigurationStore>();
        _ = store.Configuration;
        _ = store.Configuration;

        fileSystem.NumberOfCallsExists[Path].ShouldBe(1);
    }
}
