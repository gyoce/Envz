using Envz.Application.Requests.Tools;
using Envz.CommonTests.Services;
using Envz.Infrastructure.Configuration;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Envz.FunctionalTests.Infrastructure;

public class ConfigurationStoreTests : BaseTestFixture
{
    private const string Path = @"C:\Envz\configuration.json";

    protected override void ConfigureServices(IServiceCollection services)
    {
        ReplaceByMock<IConfigurationFilesPathProvider>();
        GetMock<IConfigurationFilesPathProvider>().Setup(p => p.ConfigurationFilePath).Returns(Path);
    }

    [Fact]
    public void ShouldReturnEmptyConfigurationWhenFileDoesNotExist()
    {
        IConfigurationStore store = GetService<IConfigurationStore>();

        store.Configuration.Tools.ShouldBeEmpty();
        store.Configuration.Workspaces.ShouldBeEmpty();
    }

    [Fact]
    public void ShouldLoadConfigurationFromFile()
    {
        GetServiceAs<IFileSystem, InMemoryFileSystem>().Files[Path] = """{ "applications": [ { "name": "App", "path": "C:\\app.exe" } ] }""";

        IConfigurationStore store = GetService<IConfigurationStore>();

        store.Configuration.Tools.Count.ShouldBe(1);
        store.Configuration.Tools[0].Name.ShouldBe("App");
    }

    [Fact]
    public void ShouldWriteConfigurationToFile()
    {
        IConfigurationStore store = GetService<IConfigurationStore>();
        store.Configuration.Tools.Add(new ToolDto { Name = "App", Path = "C:\\app.exe" });

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

    [Fact]
    public void ShouldWriteIconsAfterConfiguration()
    {
        IConfigurationStore store = GetService<IConfigurationStore>();
        store.Configuration.Tools.Add(new ToolDto { Name = "App", Path = "C:\\app.exe" });
        store.Configuration.Icons.ToolIcons["App"] = "AQID";

        store.Save();

        string json = GetServiceAs<IFileSystem, InMemoryFileSystem>().Files[Path];
        json.IndexOf("\"icons\"").ShouldBeGreaterThan(json.IndexOf("\"settings\""));
    }

    private static CreateToolsRequest GetCreateToolsRequest(params ToolToCreate[] tools) => new() { Tools = tools };
}
