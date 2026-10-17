using Envz.Application.Requests.Workspaces;
using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.FunctionalTests.Workspaces;

public class GetWorkspacesTests : BaseTestFixture
{
    [Fact]
    public void ShouldGetWorkspacesIfThereAreAtLeastOneWorkspaceInTheConfiguration()
    {
        SetConfiguration(ConfigurationWithTwoWorkspaces);

        GetWorkspacesRequest request = new();
        IReadOnlyCollection<Workspace> workspaces = Send(request);

        workspaces.Count.ShouldBe(2);
        workspaces.ShouldContain(e => e.Name == "Env1");
        workspaces.ShouldContain(e => e.Name == "Env2");
    }

    [Fact]
    public void ShouldMapToolsCorrectly()
    {
        SetConfiguration(ConfigurationWithTwoWorkspaces);

        IReadOnlyCollection<Workspace> workspaces = Send(new GetWorkspacesRequest());

        Workspace env1 = workspaces.Single(e => e.Name == "Env1");
        env1.Tools.Count.ShouldBe(1);
        env1.Tools[0].ToolName.ShouldBe("App1");
        env1.Tools[0].Parameter.ShouldBe("--prod");
    }

    [Fact]
    public void ShouldNotGetWorkspacesIfThereAreNoneInTheConfiguration()
    {
        SetConfiguration(ConfigurationDtoBuilder.EmptyConfiguration());

        IReadOnlyCollection<Workspace> workspaces = Send(new GetWorkspacesRequest());

        workspaces.Count.ShouldBe(0);
    }

    private static ConfigurationDto ConfigurationWithTwoWorkspaces =>
        new ConfigurationDtoBuilder()
            .WithWorkspace(new WorkspaceDtoBuilder()
                .WithName("Env1")
                .WithTool(new WorkspaceToolDtoBuilder()
                    .WithToolName("App1")
                    .WithParameter("--prod")
                    .Build())
                .Build())
            .WithWorkspace(new WorkspaceDtoBuilder()
                .WithName("Env2")
                .Build())
            .Build();
}