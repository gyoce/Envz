using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Functional.Environments;
using Envz.Infrastructure.Configuration;

namespace Envz.FunctionalTests.Environments;

public class CreateEnvironmentTests : BaseTestFixture
{
    [Fact]
    public void ShouldAddEnvironmentToConfiguration()
    {
        ConfigurationDto configuration = ConfigurationWithOneApplication("App1");
        SetConfiguration(configuration);

        Send(new CreateEnvironmentRequest
        {
            Name = "MyEnvironment",
            Applications =
            [
                new EnvironmentApplication { ApplicationName = "App1", Parameter = "--dev" }
            ]
        });

        configuration.Environments.Count.ShouldBe(1);
        configuration.Environments[0].Name.ShouldBe("MyEnvironment");
        configuration.Environments[0].Applications.Count.ShouldBe(1);
        configuration.Environments[0].Applications[0].ApplicationName.ShouldBe("App1");
        configuration.Environments[0].Applications[0].Parameter.ShouldBe("--dev");
    }

    [Fact]
    public void ShouldThrowIfUnknownApplication()
    {
        SetConfiguration(ConfigurationWithOneApplication("App1"));

        Should.Throw<ApplicationNotFoundException>(() => Send(new CreateEnvironmentRequest
        {
            Name = "MyEnvironment",
            Applications =
            [
                new EnvironmentApplication { ApplicationName = "App1", Parameter = "--dev" },
                new EnvironmentApplication { ApplicationName = "App2", Parameter = "--dev" }
            ]
        }));
    }

    [Fact]
    public void ShouldSaveConfiguration()
    {
        SetConfiguration(ConfigurationDtoBuilder.EmptyConfiguration());

        Send(new CreateEnvironmentRequest { Name = "MyEnvironment" });

        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Once);
    }

    [Fact]
    public void ShouldThrowValidationExceptionWhenNameIsNullOrWhiteSpace()
    {
        SetConfiguration(ConfigurationDtoBuilder.EmptyConfiguration());

        Should.Throw<ValidationException>(() => Send(new CreateEnvironmentRequest { Name = "   " }));
    }

    [Fact]
    public void ShouldNotSaveConfigurationWhenValidationFails()
    {
        ConfigurationDto configuration = ConfigurationDtoBuilder.EmptyConfiguration();
        SetConfiguration(configuration);

        Should.Throw<ValidationException>(() => Send(new CreateEnvironmentRequest { Name = "" }));

        configuration.Environments.ShouldBeEmpty();
        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Never);
    }

    private static ConfigurationDto ConfigurationWithOneApplication(string appName) =>
        new ConfigurationDtoBuilder()
            .WithApplication(
                new ApplicationDtoBuilder()
                    .WithName(appName)
                    .Build())
            .Build();
}