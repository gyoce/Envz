using Envz.UI.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services.Navigation;

public interface INavigationService
{
    public event Action<ViewModelBase> CurrentViewModelChanged;

    public void NavigateTo<TViewModel>(Action<TViewModel>? configure = null) where TViewModel : ViewModelBase;
}

public class NavigationService([ServiceKey] ENavigationRegion region, IServiceProvider serviceProvider) : INavigationService
{
    public event Action<ViewModelBase>? CurrentViewModelChanged;

    public void NavigateTo<TViewModel>(Action<TViewModel>? configure = null) where TViewModel : ViewModelBase
    {
        if (!region.HasViewModelTypeInside<TViewModel>())
            throw new NavigationException($"View model {typeof(TViewModel).Name} is not inside of region {region}");

        TViewModel viewModel = serviceProvider.GetRequiredService<TViewModel>();
        viewModel.OnEnable();
        CurrentViewModelChanged?.Invoke(viewModel);
    }
}