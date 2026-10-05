using Envz.Domain.Exceptions;
using Envz.Functional.Applications;
using Envz.Functional.Environments;
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

        exception.Code.ShouldBe(ExceptionCode.ConfigurationInvalid);
        Detail(exception).ShouldContain(ConfigurationPath);
    }

    [Fact]
    public void ShouldNotOverwriteFileThatIsNotValidJson()
    {
        WriteConfigurationFile(NotJson);

        Should.Throw<InvalidConfigurationException>(() => Send(new CreateApplicationRequest { Name = "App", Path = @"C:\app.exe" }));

        ReadConfigurationFile().ShouldBe(NotJson);
    }

    [Fact]
    public void ShouldNameTheInvalidApplication()
    {
        WriteConfigurationFile("""{ "applications": [ { "name": "Chrome", "path": "" } ] }""");

        InvalidConfigurationException exception = Should.Throw<InvalidConfigurationException>(() => Send(new GetApplicationsRequest()));

        exception.Code.ShouldBe(ExceptionCode.ConfigurationInvalid);
        Detail(exception).ShouldContain("Chrome");
    }

    [Fact]
    public void ShouldNameTheInvalidEnvironment()
    {
        WriteConfigurationFile("""{ "environments": [ { "name": "Dev", "applications": [ { "applicationName": "" } ] } ] }""");

        InvalidConfigurationException exception = Should.Throw<InvalidConfigurationException>(() => Send(new GetEnvironmentsRequest()));

        exception.Code.ShouldBe(ExceptionCode.ConfigurationInvalid);
        Detail(exception).ShouldContain("Dev");
    }

    [Fact]
    public void ShouldNameTheApplicationWithInvalidIcon()
    {
        WriteConfigurationFile("""{ "applications": [ { "name": "Chrome", "path": "C:\\chrome.exe" } ] }""");
        GetServiceAs<IFileSystem, InMemoryFileSystem>().Files[IconsPath] = """{ "applications": { "Chrome": "not base64!" } }""";

        InvalidConfigurationException exception = Should.Throw<InvalidConfigurationException>(() => Send(new GetApplicationsRequest()));

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