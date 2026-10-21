using Envz.Application.Requests.Tools;
using Envz.Domain.Exceptions.Tools;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.FunctionalTests.Tools;

public class CreateToolTests : BaseTestFixture
{
    [Fact]
    public void ShouldAddToolToConfiguration()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder().Build();
        SetConfiguration(configuration);

        Send(GetRequest(new ToolToCreate("MainTool", @"C:\path\app.exe")));

        configuration.Tools.Count.ShouldBe(1);
        configuration.Tools[0].Name.ShouldBe("MainTool");
        configuration.Tools[0].Path.ShouldBe(@"C:\path\app.exe");
    }

    [Fact]
    public void ShouldSaveConfiguration()
    {
        SetConfiguration(new ConfigurationDtoBuilder().Build());

        Send(GetRequest(new ToolToCreate("App", "path")));

        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Once);
    }

    [Fact]
    public void ShouldWorkForMultipleCreation()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder().Build();
        SetConfiguration(configuration);

        Send(GetRequest(
            new ToolToCreate("App1", "path1"),
            new ToolToCreate("App2", "path2")
        ));

        configuration.Tools.Count.ShouldBe(2);
        configuration.Tools[0].Name.ShouldBe("App1");
        configuration.Tools[0].Path.ShouldBe("path1");
        configuration.Tools[1].Name.ShouldBe("App2");
        configuration.Tools[1].Path.ShouldBe("path2");
    }

    [Fact]
    public void ShouldNotCreateAnyToolsIfOneIsAlreadyPresent()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder().WithTool(new ToolDtoBuilder().WithName("MyFirstApp").Build()).Build();
        SetConfiguration(configuration);

        Should.Throw<ToolAlreadyExistsException>(() => Send(GetRequest(
            new ToolToCreate("App1", "path1"),
            new ToolToCreate("MyFirstApp", "path2")
        )));

        configuration.Tools.Count.ShouldBe(1);
        configuration.Tools[0].Name.ShouldBe("MyFirstApp");
    }

    [Fact]
    public void ShouldCheckForUnicity()
    {
        SetConfiguration(new ConfigurationDtoBuilder().WithTool(new ToolDtoBuilder().WithName("App1").Build()).Build());

        Should.Throw<ToolAlreadyExistsException>(() => Send(GetRequest(new ToolToCreate("App1", "path"))));
    }

    [Fact]
    public void ShouldCheckForUnicityIgnoringCase()
    {
        SetConfiguration(new ConfigurationDtoBuilder().WithTool(new ToolDtoBuilder().WithName("MyApp1").Build()).Build());

        Should.Throw<ToolAlreadyExistsException>(() => Send(GetRequest(new ToolToCreate("myapp1", "path"))));
    }

    private static CreateToolsRequest GetRequest(params ToolToCreate[] tools) => new() { Tools = tools };
}
