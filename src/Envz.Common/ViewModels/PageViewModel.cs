using Envz.Common.Services.Navigation;

namespace Envz.Common.ViewModels;

public abstract class PageViewModel : ViewModelBase
{
    public abstract ENavigationCategory Category { get; }
    public virtual string? Title => null;
    public virtual int Level => string.IsNullOrWhiteSpace(Title) ? 0 : 1;
}