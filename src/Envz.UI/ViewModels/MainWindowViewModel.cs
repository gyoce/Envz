using System.Collections.ObjectModel;
using System.Windows.Input;

using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.Pages;
using Envz.UI.ViewModels.Pages.Applications;
using Envz.UI.ViewModels.Pages.Environments;
using Envz.UI.ViewModels.Pages.Home;
using Envz.UI.ViewModels.Pages.Settings;
using Envz.UI.ViewModels.UserControls.Controls;

namespace Envz.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand ShowHomePageCommand { get; }
    public ICommand ShowEnvironmentsPageCommand { get; }
    public ICommand ShowSettingsPageCommand { get; }
    public ICommand ShowApplicationsPageCommand { get; }
    public ObservableCollection<BreadcrumbItemViewModel> BreadcrumbItems { get; } = [];
    public bool IsDialogOpen
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }
    public PageViewModel CurrentViewModel
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = null!;

    private readonly INavigationService _navigationService;

    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        _navigationService.OnNavigationChanged += OnNavigationChanged;
        ShowHomePageCommand = new RelayCommand(_ => _navigationService.NavigateTo<HomePageViewModel>());
        ShowEnvironmentsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo<EnvironmentsPageViewModel>());
        ShowApplicationsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo<ApplicationsPageViewModel>());
        ShowSettingsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo<SettingsPageViewModel>());

        _navigationService.NavigateTo<HomePageViewModel>();
    }

    public override void Dispose()
    {
        _navigationService.OnNavigationChanged -= OnNavigationChanged;
        base.Dispose();
        GC.SuppressFinalize(this);
    }

    private void OnNavigationChanged(PageViewModel viewModel)
    {
        CurrentViewModel = viewModel;
        RebuildBreadcrumb();
    }

    private void RebuildBreadcrumb()
    {
        BreadcrumbItems.Clear();
        IReadOnlyList<PageViewModel> chain = _navigationService.Breadcrumb;

        for (int i = 0; i < chain.Count; i++)
        {
            PageViewModel page = chain[i];
            bool isLast = i == chain.Count - 1;
            bool isFirst = i == 0;

            ICommand? command = isLast
                ? null
                : new RelayCommand(_ => _navigationService.NavigateTo(page.GetType()));

            BreadcrumbItems.Add(new BreadcrumbItemViewModel(page.Title, isFirst, !isLast, command));
        }
    }
}
