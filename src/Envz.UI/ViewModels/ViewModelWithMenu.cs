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
    public string CurrentPageTitle
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = string.Empty;

    protected readonly INavigationService NavigationService;

    public ViewModelWithMenu(INavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigationService.CurrentViewModelChanged += CurrentViewModelChanged;
    }

    public override void Dispose()
    {
        NavigationService.CurrentViewModelChanged -= CurrentViewModelChanged;
        GC.SuppressFinalize(this);
    }

    private void CurrentViewModelChanged(ViewModelBase viewModel)
    {
        CurrentViewModel = viewModel;
        CurrentPageTitle = viewModel.Title;
    }
}