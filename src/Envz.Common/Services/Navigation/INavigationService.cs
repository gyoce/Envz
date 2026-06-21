using Envz.Common.ViewModels;

namespace Envz.Common.Services.Navigation;

public interface INavigationService
{
    event Action<PageViewModel> OnNavigationChanged;

    IReadOnlyList<BreadcrumbItem> Breadcrumb { get; }

    void NavigateTo<TViewModel>() where TViewModel : PageViewModel;
    void NavigateTo(Type viewModelType);
}