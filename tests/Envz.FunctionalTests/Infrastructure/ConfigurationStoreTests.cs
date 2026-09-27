using Envz.Domain.Entities;
using Envz.Functional.Applications;
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

    [Fact]
    public void ShouldSaveIconInIconStore()
    {
        IconsDto icons = new();
        SetConfiguration(new ConfigurationDtoBuilder().Build(), icons);
        byte[] icon = [1, 2, 3];

        Send(new CreateApplicationRequest { Name = "App", Path = "path", Icon = icon });

        icons.ApplicationIcons["App"].ShouldBe(Convert.ToBase64String(icon));
        GetMock<IIconStore>().Verify(store => store.Save(), Times.Once);
    }

    [Fact]
    public void ShouldNotSaveIconStoreWhenApplicationHasNoIcon()
    {
        SetConfiguration(new ConfigurationDtoBuilder().Build());

        Send(new CreateApplicationRequest { Name = "App", Path = "path", Icon = [] });

        GetMock<IIconStore>().Verify(store => store.Save(), Times.Never);
    }

    [Fact]
    public void ShouldGetIconFromIconStore()
    {
        SetConfiguration(
            new ConfigurationDtoBuilder().WithApplication(new ApplicationDtoBuilder().WithName("App1").Build()).Build(),
            new IconsDtoBuilder().WithIcon("App1", Convert.ToBase64String(new byte[] { 1, 2, 3 })).Build()
        );

        Application app = Send(new GetApplicationsRequest()).Single();

        app.Icon.ShouldBe([1, 2, 3]);
    }
}
