using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Extensions;

namespace Envz.Infrastructure.Configuration.Dtos;

public static class DtoExtensions
{
    public static Tool ToEntity(this ToolDto dto, Dictionary<string, string> icons)
    {
        try
        {
            return new Tool(
                dto.Name,
                dto.Path,
                icons.TryGetValue(dto.Name, out string? icon) && !string.IsNullOrWhiteSpace(icon) ? icon.FromBase64() : null
            );
        }
        catch (ValidationException ex)
        {
            throw new InvalidConfigurationException($"Tool \"{dto.Name}\": {ex.Message}");
        }
        catch (FormatException)
        {
            throw new InvalidConfigurationException($"Tool \"{dto.Name}\": icon is not valid base64");
        }
    }

    public static Workspace ToEntity(this WorkspaceDto dto)
    {
        try
        {
            return new Workspace(
                dto.Name,
                dto.Tools.Select(workspaceToolDto => new WorkspaceTool(workspaceToolDto.ToolName, workspaceToolDto.Parameter))
            );
        }
        catch (ValidationException ex)
        {
            throw new InvalidConfigurationException($"Workspace \"{dto.Name}\": {ex.Message}");
        }
    }

    public static ToolDto ToDto(this Tool tool)
    {
        return new ToolDto
        {
            Name = tool.Name,
            Path = tool.Path
        };
    }

    public static WorkspaceDto ToDto(this Workspace workspace)
    {
        return new WorkspaceDto
        {
            Name = workspace.Name,
            Tools = workspace.Tools.Select(workspaceTool => workspaceTool.ToDto()).ToList()
        };
    }

    public static WorkspaceToolDto ToDto(this WorkspaceTool workspaceTool)
    {
        return new WorkspaceToolDto
        {
            ToolName = workspaceTool.ToolName,
            Parameter = workspaceTool.Parameter
        };
    }
}