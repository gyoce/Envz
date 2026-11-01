using Envz.Domain.Entities;

namespace Envz.Domain.Ports;

public interface IToolRepository
{
    void Save(IReadOnlyCollection<Tool> tools);

    IReadOnlyCollection<Tool> GetAll();
    Tool? Get(string toolName);
    bool Exists(string toolName);

    void Update(string toolName, Tool newTool);

    void Delete(string toolName);
}