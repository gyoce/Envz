using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Shouldly;

namespace Envz.UnitTests.Entities;

public class WorkspaceToolTests
{
    [Fact]
    public void ShouldConstructWorkspaceToolCorrectly()
    {
        Should.NotThrow(() => new WorkspaceTool("MyApp1"));
        Should.NotThrow(() => new WorkspaceTool("MyApp2", "Parameter2"));
    }

    [Fact]
    public void ShouldThrowIfWorkspaceToolIsConstructWithInvalidValues()
    {
        Should.Throw<ValidationException>(() => new WorkspaceTool(string.Empty));
        Should.Throw<ValidationException>(() => new WorkspaceTool(string.Empty, string.Empty));
        Should.Throw<ValidationException>(() => new WorkspaceTool(string.Empty, "Parameter"));
    }
}