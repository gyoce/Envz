namespace Envz.Domain.Entities;

public class Environment
{
    public string Name { get; set; } = string.Empty;
    public List<EnvironmentApplication> Applications { get; set; } = [];
}