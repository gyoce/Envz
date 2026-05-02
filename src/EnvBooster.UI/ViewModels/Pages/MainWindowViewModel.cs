using System.Windows.Input;

using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels.Pages;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand ShowHomePageCommand { get; }
    public ICommand ShowSettingsPageCommand { get; }
    public ICommand ShowCreateEnvironmentPageCommand { get; }
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

    public MainWindowViewModel(HomePageViewModel homePageViewModel, SettingsPageViewModel settingsPageViewModel, CreateEnvironmentPageViewModel createEnvironmentPageViewModel)
    {
        ShowHomePageCommand = new RelayCommand(_ => CurrentViewModel = homePageViewModel);
        ShowSettingsPageCommand = new RelayCommand(_ => CurrentViewModel = settingsPageViewModel);
        ShowCreateEnvironmentPageCommand = new RelayCommand(_ => CurrentViewModel = createEnvironmentPageViewModel);
        CurrentViewModel = homePageViewModel;
    }
}
