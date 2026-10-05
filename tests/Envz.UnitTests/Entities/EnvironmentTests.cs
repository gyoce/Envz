using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Shouldly;

namespace Envz.UnitTests.Entities;

public class EnvironmentTests
{
    [Fact]
    public void ShouldConstructEnvironmentCorrectly()
    {
        Should.NotThrow(() => new Environment("MyApp1", []));
        Should.NotThrow(() => new Environment("MyApp2", [new EnvironmentApplication("ApplicationName")]));
    }

    [Fact]
    public void ShouldThrowIfEnvironmentIsConstructWithInvalidValues()
    {
        Should.Throw<ValidationException>(() => new Environment(string.Empty, []));
        Should.Throw<ValidationException>(() => new Environment(string.Empty, [new EnvironmentApplication("ApplicationName")]));
    }
}