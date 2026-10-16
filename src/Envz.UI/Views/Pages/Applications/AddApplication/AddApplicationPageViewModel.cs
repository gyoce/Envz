using Envz.Common.Services.Dialogs;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.UI.Utils;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Services.Extensions;
using Envz.UI.Views.Pages.Applications.AddCustomApplication;
using Envz.UI.Views.Pages.Applications.HomeApplications;
using Envz.UI.Views.UserControls.SelectableApplicationItem;
using System.Windows.Input;

namespace Envz.UI.Views.Pages.Applications.AddApplication;

public class AddApplicationPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Applications;
    public override string Title => Strings.AddApplication_Title;

    public ICommand AddSelectedCommand { get; }
    public ICommand BrowseCommand { get; }
    public ICommand CancelCommand { get; }
    public SearchableCollection<SelectableApplicationItemViewModel, SuggestedApplication> SearchableApplications { get; }
    public bool IsLoading
    {
        get;
        private set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ShowNoneDetected));
        }
    }
    public bool ShowNoneDetected => !IsLoading && !SearchableApplications.HasItems;
    public int SelectedCount => _selected.Count;
    public string AddButtonText => SelectedCount == 0 ? Strings.Global_Add : string.Format(Strings.AddApplication_AddSelected, SelectedCount);

    private readonly INavigationService _navigationService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IMediator _mediator;
    private readonly HashSet<Application> _selected = [];
    private int _loadVersion;

    public AddApplicationPageViewModel(INavigationService navigationService, IFileDialogService fileDialogService, IMediator mediator, SelectableApplicationItemViewModelFactory viewModelFactory)
    {
        _navigationService = navigationService;
        _fileDialogService = fileDialogService;
        _mediator = mediator;

        AddSelectedCommand = new RelayCommand(_ => AddSelected(), _ => SelectedCount > 0);
        BrowseCommand = new RelayCommand(_ => Browse());
        CancelCommand = new RelayCommand(_ => navigationService.NavigateTo<HomeApplicationsPageViewModel>());

        SearchableApplications = new SearchableCollection<SelectableApplicationItemViewModel, SuggestedApplication>(
            suggestion => suggestion.Application.Name,
            suggestion => viewModelFactory.Create(suggestion, OnSelectionChanged)
        );
    }

    // ReSharper disable once AsyncVoidMethod
    public override async void OnEnable()
    {
        int version = ++_loadVersion;

        _selected.Clear();
        NotifySelectionChanged();
        SearchableApplications.SearchText = string.Empty;
        SearchableApplications.UnfilteredItems = [];
        IsLoading = true;

        try
        {
            IReadOnlyCollection<SuggestedApplication> suggestions = await _mediator.SendAsync(new GetSuggestedApplicationsRequest());

            if (version != _loadVersion)
                return;

            SearchableApplications.UnfilteredItems = suggestions;
        }
        finally
        {
            if (version == _loadVersion)
                IsLoading = false;
        }
    }

    private void OnSelectionChanged(SelectableApplicationItemViewModel item)
    {
        if (item.IsSelected)
            _selected.Add(item.Application);
        else
            _selected.Remove(item.Application);

        NotifySelectionChanged();
    }

    private void NotifySelectionChanged()
    {
        OnPropertyChanged(nameof(SelectedCount));
        OnPropertyChanged(nameof(AddButtonText));
    }

    private void AddSelected()
    {
        foreach (Application application in _selected.OrderBy(app => app.Name))
        {
            _mediator.Send(new CreateApplicationRequest
            {
                Name = application.Name,
                Path = application.Path,
                Icon = application.Icon
            });
        }

        _navigationService.NavigateTo<HomeApplicationsPageViewModel>();
    }

    private void Browse()
    {
        string? path = _fileDialogService.BrowseApplication();
        if (path is null)
            return;

        _navigationService.NavigateTo<AddCustomApplicationPageViewModel>(vm => vm.InitialPath = path);
    }
}
