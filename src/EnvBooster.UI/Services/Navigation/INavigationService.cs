using EnvBooster.UI.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI.Services.Navigation;

public interface INavigationService
{
    public event Action<ViewModelBase> CurrentViewModelChanged;
    public ViewModelBase? CurrentViewModel { get; }

    public void NavigateTo(ENavigationMenu navigationMenu);
}

public class NavigationService([ServiceKey] ENavigationRegion region, IServiceProvider serviceProvider) : INavigationService
{
    public event Action<ViewModelBase>? CurrentViewModelChanged;
    public ViewModelBase? CurrentViewModel { get; private set; }

    public void NavigateTo(ENavigationMenu navigationMenu)
    {
        if (!navigationMenu.InsideOfRegion(region))
            throw new NavigationException($"{navigationMenu} is not inside of region {region}");

        ViewModelBase viewModel = (ViewModelBase)serviceProvider.GetRequiredService(navigationMenu.ToViewModelType());
        CurrentViewModel = viewModel;
        CurrentViewModel.OnEnable();
        CurrentViewModelChanged?.Invoke(viewModel);
    }
}