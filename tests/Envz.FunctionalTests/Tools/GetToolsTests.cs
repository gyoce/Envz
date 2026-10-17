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
        SetConfiguration(ConfigurationWithNoTools);

        GetToolsRequest request = new();
        IReadOnlyCollection<Tool> tools = Send(request);

        tools.Count.ShouldBe(0);
    }

    private static ConfigurationDto ConfigurationWithThreeTools =>
        new ConfigurationDtoBuilder()
            .WithTool(new ToolDtoBuilder().WithName("MainTool1").WithPath("ABC").Build())
            .WithTool(new ToolDtoBuilder().WithName("MainTool2").WithPath("ABC").Build())
            .WithTool(new ToolDtoBuilder().WithName("MainTool3").WithPath("ABC").Build())
        .Build();

    private static ConfigurationDto ConfigurationWithNoTools =>
        new ConfigurationDtoBuilder()
        .Build();
}
