using Envz.Application.Requests.Tools;
using Envz.Domain.Exceptions.Tools;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.FunctionalTests.Tools;

public class DeleteToolTests : BaseTestFixture
{
    [Fact]
    public void ShouldRemoveToolAndIcon()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder()
            .WithTool(new ToolDtoBuilder().WithName("App1"))
            .WithIcons(new IconsDtoBuilder().WithIcon("App1", "AQID"));
        SetConfiguration(configuration);

        Send(new DeleteToolRequest { ToolName = "App1" });

        configuration.Tools.ShouldBeEmpty();
        configuration.Icons.ToolIcons.ShouldNotContainKey("App1");
        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Once);
    }

    [Fact]
    public void ShouldThrowIfToolIsStillUsed()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder()
            .WithTool(new ToolDtoBuilder().WithName("App1"))
            .WithWorkspace(new WorkspaceDtoBuilder()
                .WithName("Env1")
                .WithTool(new WorkspaceToolDtoBuilder().WithToolName("App1")));
        SetConfiguration(configuration);

        Should.Throw<ToolInUseException>(() => Send(new DeleteToolRequest { ToolName = "App1" }));

        configuration.Tools.Count.ShouldBe(1);
        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Never);
    }

    [Fact]
    public void ShouldThrowIfToolDoesNotExist()
    {
        SetConfiguration(ConfigurationDtoBuilder.EmptyConfiguration());

        Should.Throw<ToolNotFoundException>(() => Send(new DeleteToolRequest { ToolName = "Unknown" }));
    }

    [Fact]
    public void ShouldRemoveToolIgnoringCase()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder()
            .WithTool(new ToolDtoBuilder().WithName("MyApp1"))
            .WithIcons(new IconsDtoBuilder().WithIcon("MyApp1", "AQID"));
        SetConfiguration(configuration);

        Send(new DeleteToolRequest { ToolName = "myapp1" });

        configuration.Tools.ShouldBeEmpty();
        configuration.Icons.ToolIcons.ShouldNotContainKey("MyApp1");
        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Once);
    }
}