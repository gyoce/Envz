using Envz.Domain.Exceptions;

namespace Envz.Domain.Entities;

public class Workspace
{
    public string Name { get; }
    public IReadOnlyList<WorkspaceTool> Tools { get; }

    public Workspace(string name, IEnumerable<WorkspaceTool> tools)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Workspace name must not be empty.", ErrorCode.WorkspaceInvalidName);

        Name = name;
        Tools = tools.ToList();
    }
}