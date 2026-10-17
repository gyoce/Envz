using Envz.Application.Mediator;
using Envz.Application.Requests.Tools;
using Envz.Common.Services;
using Envz.Common.Services.Dialogs;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.UI.Services.Extensions;
using Envz.UI.Views.Pages.Tools.HomeTools;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;

namespace Envz.UI.Views.Pages.Tools.AddCustomTool;

public class AddCustomToolPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Tools;
    public override string Title => Strings.AddCustomTool_Title;
    public override int Level => 2;

    public string? InitialPath { get; set; }

    public ICommand BrowseToolCommand { get; }
    public ICommand BrowseIconCommand { get; }
    public ICommand AddToolCommand { get; }
    public ICommand CancelAddToolCommand { get; }
    public CreateToolRequest Request
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = new();
    public ImageSource? ToolIcon => Request.Icon?.Length > 0 ? _iconExtractor.DecodeFromPngBytes(Request.Icon) : null;

    private readonly INavigationService _navigationService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IIconExtractor _iconExtractor;
    private readonly IMediator _mediator;

    public AddCustomToolPageViewModel(INavigationService navigationService, IFileDialogService fileDialogService, IIconExtractor iconExtractor, IMediator mediator)
    {
        _navigationService = navigationService;
        _fileDialogService = fileDialogService;
        _iconExtractor = iconExtractor;
        _mediator = mediator;

        AddToolCommand = new RelayCommand(_ => AddTool(), _ => CanAddTool());
        CancelAddToolCommand = new RelayCommand(_ => CancelAddTool());
        BrowseIconCommand = new RelayCommand(_ => BrowseIcon());
        BrowseToolCommand = new RelayCommand(_ => BrowseTool());
    }

    public override void OnEnable()
    {
        Request = new CreateToolRequest();
        if (InitialPath is not null)
        {
            ApplyExecutable(InitialPath);
            InitialPath = null;
        }
        OnPropertyChanged(nameof(ToolIcon));
    }

    private void AddTool()
    {
        _mediator.Send(Request);
        _navigationService.NavigateTo<HomeToolsPageViewModel>();
    }

    private bool CanAddTool()
    {
        return !string.IsNullOrWhiteSpace(Request.Path) && !string.IsNullOrWhiteSpace(Request.Name);
    }

    private void CancelAddTool()
    {
        _navigationService.NavigateTo<HomeToolsPageViewModel>();
    }

    private void BrowseIcon()
    {
        string? path = _fileDialogService.BrowseIcon();
        if (path is null)
            return;

        Request.Icon = File.ReadAllBytes(path);
        OnPropertyChanged(nameof(ToolIcon));
    }

    private void BrowseTool()
    {
        string? path = _fileDialogService.BrowseTool();
        if (path is not null)
            ApplyExecutable(path);
    }

    private void ApplyExecutable(string path)
    {
        Request.Path = path;
        Request.Name = Path.GetFileNameWithoutExtension(path);
        Request.Icon = _iconExtractor.ExtractPngBytes(path);
        OnPropertyChanged(nameof(Request));
        OnPropertyChanged(nameof(ToolIcon));
    }
}
