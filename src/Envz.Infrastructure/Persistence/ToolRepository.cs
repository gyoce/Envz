using Envz.Domain.Entities;
using Envz.Domain.Extensions;
using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.Infrastructure.Persistence;

public class ToolRepository(IConfigurationStore configurationStore, IIconStore iconStore) : IToolRepository
{
    private List<ToolDto> Tools => configurationStore.Configuration.Tools;

    public IReadOnlyCollection<Tool> GetAll()
    {
        Dictionary<string, string> icons = iconStore.Icons.ToolIcons;
        return Tools.Select(toolDto => toolDto.ToEntity(icons)).ToList();
    }

    public void Save(Tool tool)
    {
        Tools.Add(
            new ToolDto
            {
                Name = tool.Name,
                Path = tool.Path
            }
        );
        configurationStore.Save();

        if (tool.Icon is not null && tool.Icon.Length > 0)
        {
            iconStore.Icons.ToolIcons[tool.Name] = Convert.ToBase64String(tool.Icon);
            iconStore.Save();
        }
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
