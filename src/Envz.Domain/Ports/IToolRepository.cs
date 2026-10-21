using Envz.Domain.Entities;

namespace Envz.Domain.Ports;

public interface IToolRepository
{
    IReadOnlyCollection<Tool> GetAll();
    void Save(IReadOnlyCollection<Tool> tools);
    bool Exists(string toolName);
    void Delete(string toolName);
}