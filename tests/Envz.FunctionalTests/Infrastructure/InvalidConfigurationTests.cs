using Envz.Application.Requests.Tools;
using Envz.Application.Requests.Workspaces;
using Envz.CommonTests.Services;
using Envz.Domain.Exceptions;
using Envz.Infrastructure.Configuration;
using Envz.Infrastructure.Configuration.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Envz.FunctionalTests.Infrastructure;

public class InvalidConfigurationTests : BaseTestFixture
{
    private const string ConfigurationPath = @"C:\Envz\configuration.json";
    private const string IconsPath = @"C:\Envz\icons.json";
    private const string NotJson = "{ this is not json";

    protected override void ConfigureServices(IServiceCollection services)
    {
        ReplaceByMock<IConfigurationFilesPathProvider>();
        GetMock<IConfigurationFilesPathProvider>().Setup(p => p.ConfigurationFilePath).Returns(ConfigurationPath);
        GetMock<IConfigurationFilesPathProvider>().Setup(p => p.IconsFilePath).Returns(IconsPath);
    }

    [Fact]
    public void ShouldThrowWhenFileIsNotValidJson()
    {
        WriteConfigurationFile(NotJson);

        InvalidConfigurationException exception = Should.Throw<InvalidConfigurationException>(() => GetService<IConfigurationStore>().Configuration);

        exception.Code.ShouldBe(ErrorCode.ConfigurationInvalid);
        Detail(exception).ShouldContain(ConfigurationPath);
    }

    [Fact]
    public void ShouldNotOverwriteFileThatIsNotValidJson()
    {
        WriteConfigurationFile(NotJson);

        Should.Throw<InvalidConfigurationException>(() => Send(new CreateToolsRequest { Tools = [new ToolToCreate("App", "path")] }));

        ReadConfigurationFile().ShouldBe(NotJson);
    }

    [Fact]
    public void ShouldNameTheInvalidTool()
    {
        WriteConfigurationFile("""{ "applications": [ { "name": "Chrome", "path": "" } ] }""");

        InvalidConfigurationException exception = Should.Throw<InvalidConfigurationException>(() => Send(new GetToolsRequest()));

        exception.Code.ShouldBe(ErrorCode.ConfigurationInvalid);
        Detail(exception).ShouldContain("Chrome");
    }

    [Fact]
    public void ShouldNameTheInvalidWorkspace()
    {
        WriteConfigurationFile("""{ "environments": [ { "name": "Dev", "applications": [ { "applicationName": "" } ] } ] }""");

        InvalidConfigurationException exception = Should.Throw<InvalidConfigurationException>(() => Send(new GetWorkspacesRequest()));

        exception.Code.ShouldBe(ErrorCode.ConfigurationInvalid);
        Detail(exception).ShouldContain("Dev");
    }

    [Fact]
    public void ShouldNameTheToolWithInvalidIcon()
    {
        WriteConfigurationFile("""{ "applications": [ { "name": "Chrome", "path": "C:\\chrome.exe" } ] }""");
        GetServiceAs<IFileSystem, InMemoryFileSystem>().Files[IconsPath] = """{ "applications": { "Chrome": "not base64!" } }""";

        InvalidConfigurationException exception = Should.Throw<InvalidConfigurationException>(() => Send(new GetToolsRequest()));

        Detail(exception).ShouldContain("Chrome");
    }

    private void WriteConfigurationFile(string content)
    {
        GetServiceAs<IFileSystem, InMemoryFileSystem>().Files[ConfigurationPath] = content;
    }

    private string ReadConfigurationFile()
    {
        return GetServiceAs<IFileSystem, InMemoryFileSystem>().Files[ConfigurationPath];
    }

    private static string Detail(EnvzException exception)
    {
        return exception.Args.ShouldHaveSingleItem().ShouldBeOfType<string>();
    }
}