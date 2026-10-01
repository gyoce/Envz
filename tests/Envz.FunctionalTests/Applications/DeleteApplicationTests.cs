using Envz.Domain.Exceptions;
using Envz.Functional.Applications;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.FunctionalTests.Applications;

public class DeleteApplicationTests : BaseTestFixture
{
    [Fact]
    public void ShouldRemoveApplicationAndIcon()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder()
            .WithApplication(new ApplicationDtoBuilder().WithName("App1").Build())
            .Build();
        IconsDto icons = new IconsDtoBuilder().WithIcon("App1", "AQID").Build();
        SetConfiguration(configuration, icons);

        Send(new DeleteApplicationRequest { ApplicationName = "App1" });

        configuration.Applications.ShouldBeEmpty();
        icons.ApplicationIcons.ShouldNotContainKey("App1");
        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Once);
        GetMock<IIconStore>().Verify(store => store.Save(), Times.Once);
    }

    [Fact]
    public void ShouldThrowIfApplicationIsUsedByAnEnvironment()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder()
            .WithApplication(new ApplicationDtoBuilder().WithName("App1").Build())
            .WithEnvironment(new EnvironmentDtoBuilder()
                .WithName("Env1")
                .WithApplication(new EnvironmentApplicationDtoBuilder().WithApplicationName("App1").Build())
                .Build())
            .Build();
        SetConfiguration(configuration);

        Should.Throw<ApplicationInUseException>(() => Send(new DeleteApplicationRequest { ApplicationName = "App1" }));

        configuration.Applications.Count.ShouldBe(1);
        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Never);
    }

    [Fact]
    public void ShouldThrowIfApplicationDoesNotExist()
    {
        SetConfiguration(ConfigurationDtoBuilder.EmptyConfiguration());

        Should.Throw<ApplicationNotFoundException>(() => Send(new DeleteApplicationRequest { ApplicationName = "Unknown" }));
    }
}