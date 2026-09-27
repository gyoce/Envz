using Envz.Functional.Environments;
using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.FunctionalTests.Environments;

public class GetEnvironmentsTests : BaseTestFixture
{
    [Fact]
    public void ShouldGetEnvironmentsIfThereAreAtLeastOneEnvironmentInTheConfiguration()
    {
        SetConfiguration(ConfigurationWithTwoEnvironments);

        GetEnvironmentsRequest request = new();
        IReadOnlyCollection<Environment> environments = Send(request);

        environments.Count.ShouldBe(2);
        environments.ShouldContain(e => e.Name == "Env1");
        environments.ShouldContain(e => e.Name == "Env2");
    }

    [Fact]
    public void ShouldMapApplicationsCorrectly()
    {
        SetConfiguration(ConfigurationWithTwoEnvironments);

        IReadOnlyCollection<Environment> environments = Send(new GetEnvironmentsRequest());

        Environment env1 = environments.Single(e => e.Name == "Env1");
        env1.Applications.Count.ShouldBe(1);
        env1.Applications[0].ApplicationName.ShouldBe("App1");
        env1.Applications[0].Parameter.ShouldBe("--prod");
    }

    [Fact]
    public void ShouldNotGetEnvironmentsIfThereAreNoneInTheConfiguration()
    {
        SetConfiguration(ConfigurationDtoBuilder.EmptyConfiguration());

        IReadOnlyCollection<Environment> environments = Send(new GetEnvironmentsRequest());

        environments.Count.ShouldBe(0);
    }

    private static ConfigurationDto ConfigurationWithTwoEnvironments =>
        new ConfigurationDtoBuilder()
            .WithEnvironment(new EnvironmentDtoBuilder()
                .WithName("Env1")
                .WithApplication(new EnvironmentApplicationDtoBuilder()
                    .WithApplicationName("App1")
                    .WithParameter("--prod")
                    .Build())
                .Build())
            .WithEnvironment(new EnvironmentDtoBuilder()
                .WithName("Env2")
                .Build())
            .Build();
}