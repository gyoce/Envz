using System.Windows.Input;

using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.Pages;
using Envz.UI.ViewModels.Pages.Environments;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand ShowHomePageCommand { get; }
    public ICommand ShowEnvironmentsPageCommand { get; }
    public ICommand ShowSettingsPageCommand { get; }
    public ViewModelBase CurrentViewModel
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = null!;

    private readonly INavigationService _navigationService;

    public MainWindowViewModel([FromKeyedServices(ENavigationRegion.Main)] INavigationService navigationService)
    {
        _navigationService = navigationService;
        _navigationService.CurrentViewModelChanged += ChangeCurrentViewModel;
        ShowHomePageCommand = new RelayCommand(_ => _navigationService.NavigateTo<HomePageViewModel>());
        ShowEnvironmentsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo<EnvironmentsPageViewModel>());
        ShowSettingsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo<SettingsPageViewModel>());

        _navigationService.NavigateTo<HomePageViewModel>();
    }

    public override void Dispose()
    {
        _navigationService.CurrentViewModelChanged -= ChangeCurrentViewModel;
        GC.SuppressFinalize(this);
    }

    private void ChangeCurrentViewModel(ViewModelBase viewModel)
    {
        CurrentViewModel = viewModel;
    }
}
