using Envz.Domain.Exceptions;
using Shouldly;

namespace Envz.UnitTests.Entities;

public class ApplicationTests
{
    [Fact]
    public void ShouldConstructApplicationCorrectly()
    {
        Should.NotThrow(() => new Application("MyApp1", "MyPath1"));
        Should.NotThrow(() => new Application("MyApp2", "MyPath2", [1, 2, 3]));
    }

    [Theory]
    [InlineData("", "ValidPath")]
    [InlineData("ValidName", "")]
    [InlineData("", "")]
    public void ShouldThrowIfApplicationIsConstructWithInvalidValues(string name, string path)
    {
        Should.Throw<ValidationException>(() => new Application(name, path));
    }
}
