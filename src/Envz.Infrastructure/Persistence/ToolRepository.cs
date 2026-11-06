using Envz.Domain.Entities;
using Envz.Domain.Extensions;
using Envz.Domain.Ports;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;

namespace Envz.Infrastructure.Persistence;

public class ToolRepository(IConfigurationStore configurationStore) : IToolRepository
{
    private List<ToolDto> Tools => configurationStore.Configuration.Tools;
    private Dictionary<string, string> Icons => configurationStore.Configuration.Icons.ToolIcons;

    public void Save(IReadOnlyCollection<Tool> tools)
    {
        Tools.AddRange(tools.Select(tool => tool.ToDto()));
        foreach (Tool tool in tools.Where(t => t.Icon is not null))
            Icons[tool.Name] = tool.Icon!.ToBase64();
        configurationStore.Save();
    }

    public IReadOnlyCollection<Tool> GetAll()
    {
        return Tools.Select(toolDto => toolDto.ToEntity(Icons)).ToList();
    }

    public Tool? Get(string toolName)
    {
        return Tools.FirstOrDefault(tool => tool.Name.EqualsIgnoringCase(toolName))?.ToEntity(Icons);
    }

    public bool Exists(string toolName)
    {
        return Tools.Any(tool => tool.Name.EqualsIgnoringCase(toolName));
    }

    public void Update(string toolName, Tool newTool)
    {
        int index = Tools.FindIndex(tool => tool.Name.EqualsIgnoringCase(toolName));
        Tools[index] = newTool.ToDto();

        Icons.Remove(toolName);
        if (newTool.Icon is not null)
            Icons[newTool.Name] = newTool.Icon.ToBase64();
        configurationStore.Save();
    }

    public void Delete(string toolName)
    {
        Tools.RemoveAll(tool => tool.Name.EqualsIgnoringCase(toolName));
        Icons.Remove(toolName);
        configurationStore.Save();
    }
}
