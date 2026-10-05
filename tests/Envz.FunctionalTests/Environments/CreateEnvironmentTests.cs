using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Exceptions.Applications;
using Envz.Domain.Exceptions.Environments;
using Envz.Functional.Environments;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

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
                new EnvironmentApplication("App1", "--dev")
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
                new EnvironmentApplication("App1", "--dev"),
                new EnvironmentApplication("App2", "--dev"),
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

    [Fact]
    public void ShouldThrowIfEnvironmentWithSameNameAlreadyExists()
    {
        SetConfiguration(new ConfigurationDtoBuilder().WithEnvironment(new EnvironmentDtoBuilder().WithName("Env1").Build()).Build());

        Should.Throw<EnvironmentAlreadyExistsException>(() => Send(new CreateEnvironmentRequest { Name = "Env1" }));
    }

    private static ConfigurationDto ConfigurationWithOneApplication(string appName) =>
        new ConfigurationDtoBuilder()
            .WithApplication(
                new ApplicationDtoBuilder()
                    .WithName(appName)
                    .Build())
            .Build();
}