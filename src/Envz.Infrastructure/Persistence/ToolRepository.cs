using Envz.Domain.Entities;
using Envz.Domain.Extensions;
using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.Infrastructure.Persistence;

public class ToolRepository(IConfigurationStore configurationStore, IIconStore iconStore) : IToolRepository
{
    private List<ToolDto> Tools => configurationStore.Configuration.Tools;
    private Dictionary<string, string> Icons => iconStore.Icons.ToolIcons;

    public IReadOnlyCollection<Tool> GetAll()
    {
        Dictionary<string, string> icons = iconStore.Icons.ToolIcons;
        return Tools.Select(toolDto => toolDto.ToEntity(icons)).ToList();
    }

    public void Save(IReadOnlyCollection<Tool> tools)
    {
        Tools.AddRange(tools.Select(tool => new ToolDto
        {
            Name = tool.Name,
            Path = tool.Path
        }));
        configurationStore.Save();

        bool hasIcon = false;
        foreach (Tool tool in tools.Where(t => t.Icon is { Length: > 0 }))
        {
            Icons[tool.Name] = Convert.ToBase64String(tool.Icon!);
            hasIcon = true;
        }
        if (hasIcon)
            iconStore.Save();
    }

    public bool Exists(string toolName)
    {
        return Tools.Any(tool => tool.Name.EqualsIgnoringCase(toolName));
    }

    public void Delete(string toolName)
    {
        Tools.RemoveAll(tool => tool.Name.EqualsIgnoringCase(toolName));
        configurationStore.Save();
        iconStore.Icons.ToolIcons.Remove(toolName);
        iconStore.Save();
    }
}
