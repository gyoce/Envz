using Envz.Domain.Exceptions;
using Shouldly;

namespace Envz.UnitTests.Entities;

public class ToolTests
{
    [Fact]
    public void ShouldConstructToolCorrectly()
    {
        Should.NotThrow(() => new Tool("MyApp1", "MyPath1"));
        Should.NotThrow(() => new Tool("MyApp2", "MyPath2", [1, 2, 3]));
    }

    [Theory]
    [InlineData("", "ValidPath")]
    [InlineData("ValidName", "")]
    [InlineData("", "")]
    public void ShouldThrowIfToolIsConstructWithInvalidValues(string name, string path)
    {
        Should.Throw<ValidationException>(() => new Tool(name, path));
    }
}
