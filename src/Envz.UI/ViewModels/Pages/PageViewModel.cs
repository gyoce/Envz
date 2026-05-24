namespace Envz.UI.ViewModels.Pages;

public abstract class PageViewModel : ViewModelBase
{
    public abstract string Title { get; }
    public abstract Type? ParentPageType { get; }
    public virtual Type? RedirectType => null;
}