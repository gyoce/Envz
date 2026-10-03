namespace Envz.Common.Services.Navigation;

public sealed class BreadcrumbItem(Func<string> title, Type viewModelType)
{
    public string Title => title();
    public Type ViewModelType { get; } = viewModelType;
}
