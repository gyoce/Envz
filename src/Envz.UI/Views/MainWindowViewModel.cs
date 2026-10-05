using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.UI.Views.Pages.Applications.HomeApplications;
using Envz.UI.Views.Pages.Environments.HomeEnvironments;
using Envz.UI.Views.Pages.Home;
using Envz.UI.Views.Pages.Settings;
using Envz.UI.Views.UserControls.Breadcrumb;
using System.ComponentModel;
using System.Windows.Input;

namespace Envz.UI.Views;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand ShowHomePageCommand { get; }
    public ICommand ShowEnvironmentsPageCommand { get; }
    public ICommand ShowSettingsPageCommand { get; }
    public ICommand ShowApplicationsPageCommand { get; }

    public IEnumerable<BreadcrumbItemViewModel> BreadcrumbItems
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = [];
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
        ShowEnvironmentsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo<HomeEnvironmentsPageViewModel>());
        ShowApplicationsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo<HomeApplicationsPageViewModel>());
        ShowSettingsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo<SettingsPageViewModel>());

        _navigationService.NavigateTo<HomePageViewModel>();

        Localizer.Instance.PropertyChanged += OnLanguageChanged;
    }

    public override void Dispose()
    {
        Localizer.Instance.PropertyChanged -= OnLanguageChanged;
        _navigationService.OnNavigationChanged -= OnNavigationChanged;
        base.Dispose();
        GC.SuppressFinalize(this);
    }

    private void OnNavigationChanged(PageViewModel viewModel)
    {
        CurrentViewModel = viewModel;
        RebuildBreadcrumb();
    }

    private void OnLanguageChanged(object? sender, PropertyChangedEventArgs e)
    {
        RebuildBreadcrumb();
    }

    private void RebuildBreadcrumb()
    {
        List<BreadcrumbItemViewModel> items = [];
        IReadOnlyList<BreadcrumbItem> chain = _navigationService.Breadcrumb;

        for (int i = 0; i < chain.Count; i++)
        {
            BreadcrumbItem item = chain[i];
            bool isFirst = i == 0;
            bool isLast = i == chain.Count - 1;

            ICommand? command = isLast
                ? null
                : new RelayCommand(_ => _navigationService.NavigateTo(item.ViewModelType));

            items.Add(new BreadcrumbItemViewModel(item.Title, isFirst, !isLast, command));
        }

        BreadcrumbItems = items;
    }
}
