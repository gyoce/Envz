using Envz.Common.Services.Navigation;
using Envz.Common.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services;

public class NavigationService(IServiceProvider serviceProvider) : INavigationService
{
    public event Action<PageViewModel>? OnNavigationChanged;

    public IReadOnlyList<BreadcrumbItem> Breadcrumb => _breadcrumbs;
    public PageViewModel? CurrentPage { get; private set; }

    private readonly List<BreadcrumbItem> _breadcrumbs = [];
    private readonly Dictionary<PageViewModel, PageViewModel> _waitingParents = [];
    private ENavigationCategory? _currentNavigationCategory;

    public void NavigateTo<TViewModel>(Action<TViewModel>? configure = null)
        where TViewModel : PageViewModel
    {
        TViewModel viewModel = serviceProvider.GetRequiredService<TViewModel>();
        configure?.Invoke(viewModel);
        NavigateTo(viewModel);
    }

    public void NavigateTo(BreadcrumbItem item)
    {
        PageViewModel viewModel = item.Page ?? (PageViewModel)serviceProvider.GetRequiredService(item.ViewModelType);
        NavigateTo(viewModel);
    }

    public async Task<TResult?> NavigateForResultAsync<TViewModel, TResult>(Action<TViewModel>? configure = null)
        where TViewModel : ResultPageViewModel<TResult>
    {
        PageViewModel? parent = CurrentPage;

        TViewModel viewModel = serviceProvider.GetRequiredService<TViewModel>();
        configure?.Invoke(viewModel);
        if (parent is not null)
            _waitingParents[viewModel] = parent;

        Task<TResult?> resultTask = viewModel.WaitForResult();
        NavigateTo(viewModel);

        TResult? result = await resultTask;

        if (parent is not null && CurrentPage == viewModel)
            NavigateTo(parent);

        return result;
    }

    private void NavigateTo(PageViewModel viewModel)
    {
        PageViewModel? previousPage = CurrentPage;
        CurrentPage = viewModel;

        PageViewModel? abandonedParent = null;
        if (previousPage is IResultPageViewModel resultPage && previousPage != viewModel)
        {
            _waitingParents.Remove(previousPage, out abandonedParent);
            resultPage.Cancel();
        }

        List<PageViewModel> removed = UpdateBreadcrumb(viewModel);

        OnNavigationChanged?.Invoke(viewModel);
        viewModel.OnEnable();

        ReleaseOrphans(removed.Append(previousPage).Append(abandonedParent));
    }

    private List<PageViewModel> UpdateBreadcrumb(PageViewModel viewModel)
    {
        List<PageViewModel> removed = [];
        _currentNavigationCategory ??= viewModel.Category;

        if (_currentNavigationCategory != viewModel.Category)
        {
            removed.AddRange(_breadcrumbs.Select(item => item.Page).OfType<PageViewModel>());
            _breadcrumbs.Clear();
            _currentNavigationCategory = viewModel.Category;
        }

        if (_breadcrumbs.Count == 0)
            _breadcrumbs.Add(new BreadcrumbItem(() => viewModel.Category.ToBreadcrumbTitle(), viewModel.GetType()));

        if (viewModel.Level > 0)
        {
            if (_breadcrumbs.Count > viewModel.Level)
            {
                removed.AddRange(_breadcrumbs.Skip(viewModel.Level).Select(item => item.Page).OfType<PageViewModel>());
                _breadcrumbs.RemoveRange(viewModel.Level, _breadcrumbs.Count - viewModel.Level);
            }

            _breadcrumbs.Add(new BreadcrumbItem(() => viewModel.Title!, viewModel.GetType(), viewModel));
        }

        return removed;
    }

    private void ReleaseOrphans(IEnumerable<PageViewModel?> candidates)
    {
        foreach (PageViewModel page in candidates.OfType<PageViewModel>().Distinct())
        {
            bool stillUsed = page == CurrentPage || _waitingParents.ContainsValue(page) || _breadcrumbs.Any(item => item.Page == page);

            if (!stillUsed)
                page.OnRelease();
        }
    }
}