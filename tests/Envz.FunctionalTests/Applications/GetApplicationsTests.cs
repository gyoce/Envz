using Envz.Domain.Entities;
using Envz.Functional.Applications;
using Envz.Infrastructure.Configuration;

namespace Envz.FunctionalTests.Applications;

public class GetApplicationsTests : BaseTestFixture
{
    [Fact]
    public void ShouldGetApplicationsIfThereAreAtLeastOneApplicationInTheConfiguration()
    {
        SetConfiguration(ConfigurationWithThreeApplications);

        GetApplicationsRequest request = new();
        IReadOnlyCollection<Application> applications = Send(request);

        applications.Count.ShouldBe(3);
        applications.ShouldContain(a => a.Name == "MainApplication1");
        applications.ShouldContain(a => a.Name == "MainApplication2");
        applications.ShouldContain(a => a.Name == "MainApplication3");
    }

    [Fact]
    public void ShouldNotGetApplicationsIfThereAreNoneInTheConfiguration()
    {
        SetConfiguration(ConfigurationWithNoApplications);

        GetApplicationsRequest request = new();
        IReadOnlyCollection<Application> applications = Send(request);

        applications.Count.ShouldBe(0);
    }

    private static ConfigurationDto ConfigurationWithThreeApplications =>
        new ConfigurationDtoBuilder()
            .WithApplication(new ApplicationDtoBuilder().WithName("MainApplication1").Build())
            .WithApplication(new ApplicationDtoBuilder().WithName("MainApplication2").Build())
            .WithApplication(new ApplicationDtoBuilder().WithName("MainApplication3").Build())
        .Build();

    private static ConfigurationDto ConfigurationWithNoApplications =>
        new ConfigurationDtoBuilder()
        .Build();
}
