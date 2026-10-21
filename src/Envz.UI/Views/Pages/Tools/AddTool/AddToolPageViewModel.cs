using Envz.Application.Mediator;
using Envz.Application.Requests.Tools;
using Envz.Common.Services.Dialogs;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.UI.Utils;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using Envz.UI.Services;
using Envz.UI.Services.Extensions;
using Envz.UI.Views.Pages.Tools.AddCustomTool;
using Envz.UI.Views.Pages.Tools.HomeTools;
using Envz.UI.Views.UserControls.SelectableToolItem;
using System.Windows.Input;

namespace Envz.UI.Views.Pages.Tools.AddTool;

public class AddToolPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Tools;
    public override string Title => Strings.AddTool_Title;

    public ICommand AddSelectedCommand { get; }
    public ICommand BrowseCommand { get; }
    public ICommand CancelCommand { get; }
    public SearchableCollection<SelectableToolItemViewModel, SuggestedTool> SearchableTools { get; }
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
    public bool ShowNoneDetected => !IsLoading && !SearchableTools.HasItems;
    public int SelectedCount => _selected.Count;
    public string AddButtonText => SelectedCount == 0 ? Strings.Global_Add : string.Format(Strings.AddTool_AddSelected, SelectedCount);

    private readonly INavigationService _navigationService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IMediator _mediator;
    private readonly HashSet<Tool> _selected = [];
    private int _loadVersion;

    public AddToolPageViewModel(INavigationService navigationService, IFileDialogService fileDialogService, IMediator mediator, SelectableToolItemViewModelFactory viewModelFactory)
    {
        _navigationService = navigationService;
        _fileDialogService = fileDialogService;
        _mediator = mediator;

        AddSelectedCommand = new RelayCommand(_ => AddSelected(), _ => SelectedCount > 0);
        BrowseCommand = new RelayCommand(_ => Browse());
        CancelCommand = new RelayCommand(_ => navigationService.NavigateTo<HomeToolsPageViewModel>());

        SearchableTools = new SearchableCollection<SelectableToolItemViewModel, SuggestedTool>(
            suggestion => suggestion.Tool.Name,
            suggestion => viewModelFactory.Create(suggestion, OnSelectionChanged)
        );
    }

    // ReSharper disable once AsyncVoidMethod
    public override async void OnEnable()
    {
        int version = ++_loadVersion;

        _selected.Clear();
        NotifySelectionChanged();
        SearchableTools.SearchText = string.Empty;
        SearchableTools.UnfilteredItems = [];
        IsLoading = true;

        try
        {
            IReadOnlyCollection<SuggestedTool> suggestions = await _mediator.SendAsync(new GetSuggestedToolsRequest());

            if (version != _loadVersion)
                return;

            SearchableTools.UnfilteredItems = suggestions;
        }
        finally
        {
            if (version == _loadVersion)
                IsLoading = false;
        }
    }

    private void OnSelectionChanged(SelectableToolItemViewModel item)
    {
        if (item.IsSelected)
            _selected.Add(item.Tool);
        else
            _selected.Remove(item.Tool);

        NotifySelectionChanged();
    }

    private void NotifySelectionChanged()
    {
        OnPropertyChanged(nameof(SelectedCount));
        OnPropertyChanged(nameof(AddButtonText));
    }

    private void AddSelected()
    {
        _mediator.Send(new CreateToolsRequest { Tools = _selected.Select(t => new ToolToCreate(t.Name, t.Path, t.Icon)).ToList() });
        _navigationService.NavigateTo<HomeToolsPageViewModel>();
    }

    private void Browse()
    {
        string? path = _fileDialogService.BrowseTool();
        if (path is null)
            return;

        _navigationService.NavigateTo<AddCustomToolPageViewModel>(vm => vm.InitialPath = path);
    }
}
