using System.Windows.Input;

using EnvBooster.UI.Utils;
using EnvBooster.UI.ViewModels.Pages;
using EnvBooster.UI.ViewModels.Pages.Environments;

namespace EnvBooster.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand ShowHomePageCommand { get; }
    public ICommand ShowSettingsPageCommand { get; }
    public ICommand ShowEnvironmentsPageCommand { get; }
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    private ViewModelBase _currentViewModel = null!;

    public MainWindowViewModel(HomePageViewModel homePageViewModel, SettingsPageViewModel settingsPageViewModel, EnvironmentsPageViewModel environmentsPageViewModel)
    {
        ShowHomePageCommand = new RelayCommand(_ => CurrentViewModel = homePageViewModel);
        ShowSettingsPageCommand = new RelayCommand(_ => CurrentViewModel = settingsPageViewModel);
        ShowEnvironmentsPageCommand = new RelayCommand(_ => CurrentViewModel = environmentsPageViewModel);
        CurrentViewModel = homePageViewModel;
    }
}
