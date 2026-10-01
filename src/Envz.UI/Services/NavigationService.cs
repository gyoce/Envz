using Envz.Common.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services;

public class NavigationService(IServiceProvider serviceProvider) : INavigationService
{
    public event Action<PageViewModel>? OnNavigationChanged;

    public IReadOnlyList<BreadcrumbItem> Breadcrumb => _breadcrumbs;
    public PageViewModel? CurrentPage { get; private set; }

    private readonly List<BreadcrumbItem> _breadcrumbs = [];

    private ENavigationCategory? _currentNavigationCategory;

    public void NavigateTo<TViewModel>(Action<TViewModel>? configure = null)
        where TViewModel : PageViewModel
    {
        TViewModel viewModel = serviceProvider.GetRequiredService<TViewModel>();
        configure?.Invoke(viewModel);
        NavigateTo(viewModel, typeof(TViewModel));
    }

    public void NavigateTo(Type viewModelType)
    {
        if (!typeof(PageViewModel).IsAssignableFrom(viewModelType))
            throw new ArgumentException($"Typeof '{viewModelType}' is not one of {nameof(PageViewModel)}.", nameof(viewModelType));

        PageViewModel viewModel = (PageViewModel)serviceProvider.GetRequiredService(viewModelType);
        NavigateTo(viewModel, viewModelType);
    }

    public async Task<TResult?> NavigateForResultAsync<TViewModel, TResult>(Action<TViewModel>? configure = null)
        where TViewModel : ResultPageViewModel<TResult>
    {
        PageViewModel? parent = CurrentPage;

        TViewModel viewModel = serviceProvider.GetRequiredService<TViewModel>();
        configure?.Invoke(viewModel);
        Task<TResult?> resultTask = viewModel.WaitForResult();
        NavigateTo(viewModel, typeof(TViewModel));

        TResult? result = await resultTask;

        if (parent is not null && CurrentPage == viewModel)
            NavigateTo(parent, parent.GetType());

        return result;
    }

    private void NavigateTo(PageViewModel viewModel, Type viewModelType)
    {
        PageViewModel? previousPage = CurrentPage;
        CurrentPage = viewModel;

        if (previousPage is IResultPageViewModel resultPage && previousPage != viewModel)
            resultPage.Cancel();

        UpdateBreadcrumb(viewModel, viewModelType);

        OnNavigationChanged?.Invoke(viewModel);
        viewModel.OnEnable();
    }

    private void UpdateBreadcrumb(PageViewModel viewModel, Type viewModelType)
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
    }
}
