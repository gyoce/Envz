using Envz.Common.ViewModels;

namespace Envz.Common.Services.Navigation;

public interface INavigationService
{
    event Action<PageViewModel> OnNavigationChanged;

    IReadOnlyList<BreadcrumbItem> Breadcrumb { get; }
    PageViewModel? CurrentPage { get; }

    void NavigateTo<TViewModel>(Action<TViewModel>? configure = null) where TViewModel : PageViewModel;
    void NavigateTo(Type viewModelType);
    Task<TResult?> NavigateForResultAsync<TViewModel, TResult>(Action<TViewModel>? configure = null) where TViewModel : ResultPageViewModel<TResult>;
}