namespace Envz.Domain.Entities;

public class Application
{
    public required string Name { get; set; }
    public required string Path { get; set; }
    public byte[]? Icon { get; set; }
}