using Envz.Application.Requests.Tools;
using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.FunctionalTests.Tools;

public class GetToolsTests : BaseTestFixture
{
    [Fact]
    public void ShouldGetToolsIfThereAreAtLeastOneToolInTheConfiguration()
    {
        SetConfiguration(ConfigurationWithThreeTools);

        GetToolsRequest request = new();
        IReadOnlyCollection<Tool> tools = Send(request);

        tools.Count.ShouldBe(3);
        tools.ShouldContain(a => a.Name == "MainTool1");
        tools.ShouldContain(a => a.Name == "MainTool2");
        tools.ShouldContain(a => a.Name == "MainTool3");
    }

    [Fact]
    public void ShouldNotGetToolsIfThereAreNoneInTheConfiguration()
    {
        SetConfiguration(ConfigurationDtoBuilder.EmptyConfiguration());

        GetToolsRequest request = new();
        IReadOnlyCollection<Tool> tools = Send(request);

        tools.Count.ShouldBe(0);
    }

    [Fact]
    public void ShouldGetIcon()
    {
        SetConfiguration(
            new ConfigurationDtoBuilder()
                .WithTool(new ToolDtoBuilder().WithName("App1").WithPath("ABC"))
                .WithIcons(new IconsDtoBuilder().WithIcon("App1", Convert.ToBase64String(new byte[] { 1, 2, 3 }))
            )
        );

        Tool app = Send(new GetToolsRequest()).Single();

        app.Icon.ShouldBe([1, 2, 3]);
    }

    private static ConfigurationDto ConfigurationWithThreeTools =>
        new ConfigurationDtoBuilder()
            .WithTool(new ToolDtoBuilder().WithName("MainTool1").WithPath("ABC"))
            .WithTool(new ToolDtoBuilder().WithName("MainTool2").WithPath("ABC"))
            .WithTool(new ToolDtoBuilder().WithName("MainTool3").WithPath("ABC"));
}
