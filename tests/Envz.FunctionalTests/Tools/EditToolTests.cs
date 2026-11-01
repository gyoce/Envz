using Envz.Application.Requests.Tools;
using Envz.Domain.Exceptions.Tools;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.FunctionalTests.Tools;

public class EditToolTests : BaseTestFixture
{
    [Fact]
    public void ShouldEditToolCorrectly()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder().WithTool(new ToolDto { Name = "App1", Path = "Path1" });
        SetConfiguration(configuration);

        Send(new EditToolRequest { ToolName = "app1", NewName = "App2", NewPath = "Path2" });

        configuration.Tools.Count.ShouldBe(1);
        configuration.Tools[0].Name.ShouldBe("App2");
        configuration.Tools[0].Path.ShouldBe("Path2");
    }

    [Fact]
    public void ShouldEditToolWithIconCorrectly()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder().WithTools(new ToolDto { Name = "App1", Path = "Path1" });
        IconsDto icons = new IconsDtoBuilder().WithIcon("App1", "AQID");
        SetConfiguration(configuration, icons);

        Send(new EditToolRequest { ToolName = "App1", NewName = "App2", NewPath = "Path2", NewIcon = [1, 2, 3, 4] });

        configuration.Tools.Count.ShouldBe(1);
        configuration.Tools[0].Name.ShouldBe("App2");
        configuration.Tools[0].Path.ShouldBe("Path2");
        icons.ToolIcons.ShouldNotContainKey("App1");
        icons.ToolIcons.ShouldContainKey("App2");
        icons.ToolIcons["App2"].ShouldBe("AQIDBA=="); // bytes [1, 2, 3, 4]
    }

    [Fact]
    public void ShouldDeleteIconIfEditToolDeleteIcon()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder().WithTool(new ToolDto { Name = "App1", Path = "Path1" });
        IconsDto icons = new IconsDtoBuilder().WithIcon("App1", "AQID");
        SetConfiguration(configuration, icons);

        Send(new EditToolRequest { ToolName = "App1", NewName = "App2", NewPath = "Path2", NewIcon = null });

        configuration.Tools.Count.ShouldBe(1);
        configuration.Tools[0].Name.ShouldBe("App2");
        configuration.Tools[0].Path.ShouldBe("Path2");
        icons.ToolIcons.ShouldNotContainKey("App1");
        icons.ToolIcons.ShouldNotContainKey("App2");
    }

    [Fact]
    public void ShouldEditWorkspacesUsedWithTool()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder()
            .WithTool(new ToolDtoBuilder().WithName("App1").WithPath("Path1"))
            .WithWorkspace(new WorkspaceDtoBuilder().WithName("Workspace1").WithTool(new WorkspaceToolDtoBuilder().WithToolName("App1")));
        SetConfiguration(configuration);

        Send(new EditToolRequest { ToolName = "App1", NewName = "App2", NewPath = "Path2" });

        configuration.Tools.Count.ShouldBe(1);
        configuration.Tools[0].Name.ShouldBe("App2");
        configuration.Tools[0].Path.ShouldBe("Path2");
        configuration.Workspaces.Count.ShouldBe(1);
        configuration.Workspaces[0].Name.ShouldBe("Workspace1");
        configuration.Workspaces[0].Tools[0].ToolName.ShouldBe("App2");
    }

    [Fact]
    public void ShouldSaveConfigurationOnceWhenNameIsUnchanged()
    {
        SetConfiguration(new ConfigurationDtoBuilder()
            .WithTool(new ToolDtoBuilder().WithName("App1").WithPath("Path1"))
            .WithWorkspace(new WorkspaceDtoBuilder().WithName("W1").WithTool(new WorkspaceToolDtoBuilder().WithToolName("App1"))));

        Send(new EditToolRequest { ToolName = "App1", NewName = "App1", NewPath = "Path2" });

        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Once);
    }

    [Fact]
    public void ShouldNotThrowWhenNameIsUnchanged()
    {
        SetConfiguration(new ConfigurationDtoBuilder().WithTool(new ToolDtoBuilder().WithName("App1").WithPath("Path1")));

        Should.NotThrow(() => Send(new EditToolRequest { ToolName = "App1", NewName = "App1", NewPath = "Path2" }));
    }

    [Theory]
    [InlineData("App2")]
    [InlineData("  app2 ")]
    public void ShouldThrowWhenRenamingToAnotherExistingTool(string newName)
    {
        SetConfiguration(new ConfigurationDtoBuilder().WithTools(
            new ToolDtoBuilder().WithName("App1").WithPath("Path1"),
            new ToolDtoBuilder().WithName("App2").WithPath("Path2")
        ));

        Should.Throw<ToolAlreadyExistsException>(() => Send(new EditToolRequest { ToolName = "App1", NewName = newName, NewPath = "Path1" }));
    }
}