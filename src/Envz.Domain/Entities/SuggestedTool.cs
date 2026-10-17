namespace Envz.Domain.Entities;

public class SuggestedTool(Tool tool, bool isAlreadyAdded)
{
    public Tool Tool { get; } = tool;
    public bool IsAlreadyAdded { get; } = isAlreadyAdded;
}
