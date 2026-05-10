using Envz.UI.Services.Navigation;

namespace Envz.UI.ViewModels;

public class ViewModelWithMenu : ViewModelBase
{
    public ViewModelBase CurrentViewModel
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = null!;

    protected readonly INavigationService NavigationService;

    public ViewModelWithMenu(INavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigationService.CurrentViewModelChanged += CurrentSubViewModelChanged;
    }

    public override void Dispose()
    {
        NavigationService.CurrentViewModelChanged -= CurrentSubViewModelChanged;
        GC.SuppressFinalize(this);
    }

    private void CurrentSubViewModelChanged(ViewModelBase viewModel)
    {
        CurrentViewModel = viewModel;
    }
}