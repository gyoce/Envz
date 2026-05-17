namespace Envz.Domain.Entities;

public class Application
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Path { get; set; }
    public byte[]? Icon { get; set; }
}