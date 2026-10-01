using Envz.Domain.Exceptions;
using Envz.Functional.Applications;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.FunctionalTests.Applications;

public class CreateApplicationTests : BaseTestFixture
{
    [Fact]
    public void ShouldAddApplicationToConfiguration()
    {
        ConfigurationDto configuration = new ConfigurationDtoBuilder().Build();
        SetConfiguration(configuration);

        Send(new CreateApplicationRequest
        {
            Name = "MainApplication",
            Path = @"C:\path\app.exe"
        });

        configuration.Applications.Count.ShouldBe(1);
        configuration.Applications[0].Name.ShouldBe("MainApplication");
        configuration.Applications[0].Path.ShouldBe(@"C:\path\app.exe");
    }

    [Fact]
    public void ShouldSaveConfiguration()
    {
        SetConfiguration(new ConfigurationDtoBuilder().Build());

        Send(new CreateApplicationRequest { Name = "App", Path = "path" });

        GetMock<IConfigurationStore>().Verify(store => store.Save(), Times.Once);
    }

    [Fact]
    public void ShouldCheckForUnicity()
    {
        SetConfiguration(new ConfigurationDtoBuilder().WithApplication(new ApplicationDtoBuilder().WithName("App1").Build()).Build());

        Should.Throw<ApplicationAlreadyExistsException>(() => Send(new CreateApplicationRequest { Name = "App1", Path = "path" }));
    }
}
