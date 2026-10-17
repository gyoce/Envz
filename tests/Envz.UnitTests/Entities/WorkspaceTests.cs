using Envz.Domain.Exceptions;
using Shouldly;

namespace Envz.UnitTests.Entities;

public class WorkspaceTests
{
    [Fact]
    public void ShouldConstructWorkspaceCorrectly()
    {
        Should.NotThrow(() => new Workspace("MyApp1", []));
        Should.NotThrow(() => new Workspace("MyApp2", [new WorkspaceTool("ToolName")]));
    }

    [Fact]
    public void ShouldThrowIfWorkspaceIsConstructWithInvalidValues()
    {
        Should.Throw<ValidationException>(() => new Workspace(string.Empty, []));
        Should.Throw<ValidationException>(() => new Workspace(string.Empty, [new WorkspaceTool("ToolName")]));
    }
}