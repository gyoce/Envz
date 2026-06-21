using Envz.Common.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.Common.Services.Navigation;

public class NavigationService(IServiceProvider serviceProvider) : INavigationService
{
    public event Action<PageViewModel>? OnNavigationChanged;

    private ENavigationCategory? _currentNavigationCategory;

    public IReadOnlyList<BreadcrumbItem> Breadcrumb => _breadcrumbs;
    private readonly List<BreadcrumbItem> _breadcrumbs = [];

    public void NavigateTo<TViewModel>() where TViewModel : PageViewModel
    {
        NavigateTo(serviceProvider.GetRequiredService<TViewModel>(), typeof(TViewModel));
    }

    public void NavigateTo(Type viewModelType)
    {
        if (!typeof(PageViewModel).IsAssignableFrom(viewModelType))
            throw new ArgumentException($"Le type '{viewModelType}' n'est pas un {nameof(PageViewModel)}.", nameof(viewModelType));

        PageViewModel viewModel = (PageViewModel)serviceProvider.GetRequiredService(viewModelType);
        NavigateTo(viewModel, viewModelType);
    }

    private void NavigateTo(PageViewModel viewModel, Type viewModelType)
    {
        _currentNavigationCategory ??= viewModel.Category;

        if (_currentNavigationCategory != viewModel.Category)
        {
            _breadcrumbs.Clear();
            _currentNavigationCategory = viewModel.Category;
        }

        if (_breadcrumbs.Count == 0)
            _breadcrumbs.Add(new BreadcrumbItem(viewModel.Category.ToBreadcrumbTitle(), viewModelType));

        if (viewModel.Level > 0)
        {
            if (_breadcrumbs.Count > viewModel.Level)
                _breadcrumbs.RemoveRange(viewModel.Level, _breadcrumbs.Count - viewModel.Level);

            _breadcrumbs.Add(new BreadcrumbItem(viewModel.Title!, viewModelType));
        }

        OnNavigationChanged?.Invoke(viewModel);
        viewModel.OnEnable();
    }
}
