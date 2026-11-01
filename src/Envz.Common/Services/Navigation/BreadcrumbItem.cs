using Envz.Common.ViewModels;

namespace Envz.Common.Services.Navigation;

public sealed class BreadcrumbItem(Func<string> title, Type viewModelType, PageViewModel? page = null)
{
    public string Title => title();
    public Type ViewModelType { get; } = viewModelType;
    public PageViewModel? Page { get; } = page;
}
