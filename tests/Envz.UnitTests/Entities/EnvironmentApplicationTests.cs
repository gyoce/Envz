using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Shouldly;

namespace Envz.UnitTests.Entities;

public class EnvironmentApplicationTests
{
    [Fact]
    public void ShouldConstructEnvironmentApplicationCorrectly()
    {
        Should.NotThrow(() => new EnvironmentApplication("MyApp1"));
        Should.NotThrow(() => new EnvironmentApplication("MyApp2", "Parameter2"));
    }

    [Fact]
    public void ShouldThrowIfEnvironmentApplicationIsConstructWithInvalidValues()
    {
        Should.Throw<ValidationException>(() => new EnvironmentApplication(string.Empty));
        Should.Throw<ValidationException>(() => new EnvironmentApplication(string.Empty, string.Empty));
        Should.Throw<ValidationException>(() => new EnvironmentApplication(string.Empty, "Parameter"));
    }
}