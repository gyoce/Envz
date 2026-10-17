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

        Send(new CreateToolRequest
        {
            Name = "MainTool",
            Path = @"C:\path\app.exe"
        });

        configuration.Tools.Count.ShouldBe(1);
        configuration.Tools[0].Name.ShouldBe("MainTool");
        configuration.Tools[0].Path.ShouldBe(@"C:\path\app.exe");
    }

    [Fact]
    public void ShouldSaveConfiguration()
    {
        SetConfiguration(new ConfigurationDtoBuilder().Build());

        Send(new CreateToolRequest { Name = "App", Path = "path" });

        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Once);
    }

    [Fact]
    public void ShouldCheckForUnicity()
    {
        SetConfiguration(new ConfigurationDtoBuilder().WithTool(new ToolDtoBuilder().WithName("App1").Build()).Build());

        Should.Throw<ToolAlreadyExistsException>(() => Send(new CreateToolRequest { Name = "App1", Path = "path" }));
    }

    [Fact]
    public void ShouldCheckForUnicityIgnoringCase()
    {
        SetConfiguration(new ConfigurationDtoBuilder().WithTool(new ToolDtoBuilder().WithName("MyApp1").Build()).Build());

        Should.Throw<ToolAlreadyExistsException>(() => Send(new CreateToolRequest { Name = "myapp1", Path = "path" }));
    }
}
