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
    public void ShouldSaveIconInIconStore()
    {
        IconsDto icons = new();
        SetConfiguration(new ConfigurationDtoBuilder().Build(), icons);
        byte[] icon = [1, 2, 3];

        Send(GetCreateToolsRequest(new ToolToCreate("App", "path", icon)));

        icons.ToolIcons["App"].ShouldBe(Convert.ToBase64String(icon));
        GetMock<IIconStore>().Verify(store => store.Save(), Times.Once);
    }

    [Fact]
    public void ShouldNotSaveIconStoreWhenToolHasNoIcon()
    {
        SetConfiguration(new ConfigurationDtoBuilder().Build());

        Send(GetCreateToolsRequest(new ToolToCreate("App", "path")));

        GetMock<IIconStore>().Verify(store => store.Save(), Times.Never);
    }

    [Fact]
    public void ShouldGetIconFromIconStore()
    {
        SetConfiguration(
            new ConfigurationDtoBuilder().WithTool(new ToolDtoBuilder().WithName("App1").WithPath("ABC").Build()).Build(),
            new IconsDtoBuilder().WithIcon("App1", Convert.ToBase64String(new byte[] { 1, 2, 3 })).Build()
        );

        Tool app = Send(new GetToolsRequest()).Single();

        app.Icon.ShouldBe([1, 2, 3]);
    }

    private static CreateToolsRequest GetCreateToolsRequest(params ToolToCreate[] tools) => new() { Tools = tools };
}
