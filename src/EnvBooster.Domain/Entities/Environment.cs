namespace EnvBooster.Domain.Entities;

public class Environment(string id, string name)
{
    public string Id { get; } = id;
    public string Name { get; } = name;
}