using EnvBooster.UI.Services.Navigation;

using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI.ViewModels.Pages.Environments;

public class EnvironmentsPageViewModel : ViewModelBase
{
    public ViewModelBase CurrentSubViewModel
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = null!;

    private readonly INavigationService _navigationService;

    public EnvironmentsPageViewModel([FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService)
    {
        _navigationService = navigationService;
        _navigationService.CurrentViewModelChanged += CurrentSubViewModelChanged;
        _navigationService.NavigateTo<HomeEnvironmentsSubPageViewModel>();
    }

    public override void Dispose()
    {
        _navigationService.CurrentViewModelChanged -= CurrentSubViewModelChanged;
        GC.SuppressFinalize(this);
    }

    private void CurrentSubViewModelChanged(ViewModelBase viewModel)
    {
        CurrentSubViewModel = viewModel;
    }
}