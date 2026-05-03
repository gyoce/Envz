using System.Windows.Input;

using EnvBooster.UI.Utils;
using EnvBooster.UI.ViewModels.Pages.Environments.SubPages;

namespace EnvBooster.UI.ViewModels.Pages.Environments;

public class EnvironmentsPageViewModel : ViewModelBase
{
    public ICommand ShowEditEnvironmentCommand { get; }
    public ICommand ShowHomeEnvironmentsCommand { get; }
    public ViewModelBase CurrentSubViewModel
    {
        get => _currentSubViewModel;
        set
        {
            _currentSubViewModel = value;
            OnPropertyChanged();
        }
    }

    private ViewModelBase _currentSubViewModel = null!;

    public EnvironmentsPageViewModel(HomeEnvironmentsSubPageViewModel homeEnvironmentsSubPageViewModel, EditEnvironmentSubPageViewModel editEnvironmentSubPageViewModel)
    {
        ShowEditEnvironmentCommand = new RelayCommand(_ => CurrentSubViewModel = editEnvironmentSubPageViewModel);
        ShowHomeEnvironmentsCommand = new RelayCommand(_ => CurrentSubViewModel = homeEnvironmentsSubPageViewModel);
        CurrentSubViewModel = homeEnvironmentsSubPageViewModel;
    }
}