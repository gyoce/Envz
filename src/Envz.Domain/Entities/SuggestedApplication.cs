namespace Envz.Domain.Entities;

public class SuggestedApplication(Application application, bool isAlreadyAdded)
{
    public Application Application { get; } = application;
    public bool IsAlreadyAdded { get; } = isAlreadyAdded;
}
