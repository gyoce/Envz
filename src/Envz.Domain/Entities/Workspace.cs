using Envz.Domain.Exceptions;
using Envz.Domain.Extensions;

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

    public bool Uses(string toolName)
    {
        return Tools.Any(tool => tool.ToolName.EqualsIgnoringCase(toolName));
    }

    public Workspace WithRenamedTool(string oldName, string newName)
    {
        return new Workspace(Name, Tools.Select(tool =>
        {
            if (tool.ToolName.EqualsIgnoringCase(oldName))
                return new WorkspaceTool(newName, tool.Parameter);
            return tool;
        }));
    }
}