namespace Envz.Domain.Entities;

public class Environment
{
    public required string Name { get; set; }
    public List<EnvironmentApplication> Applications { get; set; } = [];
}