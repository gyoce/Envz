namespace Envz.Domain.Entities;

public class Environment
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<EnvironmentApplication> Applications { get; set; } = [];
}