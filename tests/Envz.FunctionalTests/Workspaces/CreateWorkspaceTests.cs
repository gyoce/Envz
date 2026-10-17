using Envz.Application.Requests.Workspaces;
using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Exceptions.Tools;
using Envz.Domain.Exceptions.Workspaces;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.FunctionalTests.Workspaces;

public class CreateWorkspaceTests : BaseTestFixture
{
    [Fact]
    public void ShouldAddWorkspaceToConfiguration()
    {
        ConfigurationDto configuration = ConfigurationWithOneTool("App1");
        SetConfiguration(configuration);

        Send(new CreateWorkspaceRequest
        {
            Name = "MyWorkspace",
            Tools =
            [
                new WorkspaceTool("App1", "--dev")
            ]
        });

        configuration.Workspaces.Count.ShouldBe(1);
        configuration.Workspaces[0].Name.ShouldBe("MyWorkspace");
        configuration.Workspaces[0].Tools.Count.ShouldBe(1);
        configuration.Workspaces[0].Tools[0].ToolName.ShouldBe("App1");
        configuration.Workspaces[0].Tools[0].Parameter.ShouldBe("--dev");
    }

    [Fact]
    public void ShouldThrowIfUnknownTool()
    {
        SetConfiguration(ConfigurationWithOneTool("App1"));

        Should.Throw<ToolNotFoundException>(() => Send(new CreateWorkspaceRequest
        {
            Name = "MyWorkspace",
            Tools =
            [
                new WorkspaceTool("App1", "--dev"),
                new WorkspaceTool("App2", "--dev"),
            ]
        }));
    }

    [Fact]
    public void ShouldSaveConfiguration()
    {
        SetConfiguration(ConfigurationDtoBuilder.EmptyConfiguration());

        Send(new CreateWorkspaceRequest { Name = "MyWorkspace" });

        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Once);
    }

    [Fact]
    public void ShouldThrowValidationExceptionWhenNameIsNullOrWhiteSpace()
    {
        SetConfiguration(ConfigurationDtoBuilder.EmptyConfiguration());

        Should.Throw<ValidationException>(() => Send(new CreateWorkspaceRequest { Name = "   " }));
    }

    [Fact]
    public void ShouldNotSaveConfigurationWhenValidationFails()
    {
        ConfigurationDto configuration = ConfigurationDtoBuilder.EmptyConfiguration();
        SetConfiguration(configuration);

        Should.Throw<ValidationException>(() => Send(new CreateWorkspaceRequest { Name = "" }));

        configuration.Workspaces.ShouldBeEmpty();
        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Never);
    }

    [Fact]
    public void ShouldThrowIfWorkspaceWithSameNameAlreadyExists()
    {
        SetConfiguration(new ConfigurationDtoBuilder().WithWorkspace(new WorkspaceDtoBuilder().WithName("Env1").Build()).Build());

        Should.Throw<WorkspaceAlreadyExistsException>(() => Send(new CreateWorkspaceRequest { Name = "Env1" }));
    }

    [Fact]
    public void ShouldThrowIfWorkspaceWithSameNameAlreadyExistsIgnoringCase()
    {
        SetConfiguration(new ConfigurationDtoBuilder().WithWorkspace(new WorkspaceDtoBuilder().WithName("MyEnv1").Build()).Build());

        Should.Throw<WorkspaceAlreadyExistsException>(() => Send(new CreateWorkspaceRequest { Name = "myenv1" }));
    }

    private static ConfigurationDto ConfigurationWithOneTool(string appName) =>
        new ConfigurationDtoBuilder()
            .WithTool(
                new ToolDtoBuilder()
                    .WithName(appName)
                    .Build())
            .Build();
}