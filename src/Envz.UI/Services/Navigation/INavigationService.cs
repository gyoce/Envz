using System.Reflection;

using Envz.UI.Views.Pages;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services.Navigation;

public interface INavigationService
{
    public event Action<PageViewModel> OnNavigationChanged;

    public IReadOnlyList<PageViewModel> Breadcrumb { get; }

    public void NavigateTo<TViewModel>(Action<TViewModel>? configure = null) where TViewModel : PageViewModel;
    public void NavigateTo(Type pageViewModel);
}

public class NavigationService(IServiceProvider serviceProvider) : INavigationService
{
    public event Action<PageViewModel>? OnNavigationChanged;

    public IReadOnlyList<PageViewModel> Breadcrumb => _breadcrumb;

    private List<PageViewModel> _breadcrumb = [];

    public void NavigateTo<TViewModel>(Action<TViewModel>? configure = null) where TViewModel : PageViewModel
    {
        NavigateToInternal(configure);
    }

    public void NavigateTo(Type pageViewModel)
    {
        MethodInfo method = typeof(NavigationService).GetMethod(nameof(NavigateToInternal), BindingFlags.NonPublic | BindingFlags.Instance)!.MakeGenericMethod(pageViewModel);
        method.Invoke(this, [null]);
    }

    private void NavigateToInternal<TViewModel>(Action<TViewModel>? configure = null) where TViewModel : PageViewModel
    {
        TViewModel viewModel = serviceProvider.GetRequiredService<TViewModel>();

        if (viewModel.RedirectType is not null)
        {
            MethodInfo method = typeof(NavigationService).GetMethod(nameof(NavigateToInternal), BindingFlags.NonPublic | BindingFlags.Instance)!.MakeGenericMethod(viewModel.RedirectType);
            method.Invoke(this, [null]);
            return;
        }

        BuildBreadcrumb(viewModel);
        configure?.Invoke(viewModel);
        viewModel.OnEnable();
        OnNavigationChanged?.Invoke(viewModel);
    }

    private void BuildBreadcrumb(PageViewModel page)
    {
        _breadcrumb.Clear();

        Stack<PageViewModel> stack = new();
        PageViewModel? current = page;
        while (current is not null)
        {
            stack.Push(current);
            current = current.ParentPageType is null
                ? null
                : (PageViewModel)serviceProvider.GetRequiredService(current.ParentPageType);
        }

        _breadcrumb = stack.ToList();
    }
}