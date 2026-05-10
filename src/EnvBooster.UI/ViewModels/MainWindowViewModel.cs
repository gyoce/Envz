using System.Windows.Input;

using EnvBooster.UI.Services.Navigation;
using EnvBooster.UI.Utils;

using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI.ViewModels;

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
        ShowHomePageCommand = new RelayCommand(_ => _navigationService.NavigateTo(ENavigationMenu.HomePage));
        ShowEnvironmentsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo(ENavigationMenu.EnvironmentsPage));
        ShowSettingsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo(ENavigationMenu.SettingsPage));

        _navigationService.NavigateTo(ENavigationMenu.HomePage);
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
