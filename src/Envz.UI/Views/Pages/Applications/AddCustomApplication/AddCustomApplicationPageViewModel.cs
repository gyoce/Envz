using Envz.Common.Services;
using Envz.Common.Services.Dialogs;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Services.Extensions;
using Envz.UI.Views.Pages.Applications.HomeApplications;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;

namespace Envz.UI.Views.Pages.Applications.AddCustomApplication;

public class AddCustomApplicationPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Applications;
    public override string Title => Strings.AddCustomApplication_Title;
    public override int Level => 2;

    public string? InitialPath { get; set; }

    public ICommand BrowseApplicationCommand { get; }
    public ICommand BrowseIconCommand { get; }
    public ICommand AddApplicationCommand { get; }
    public ICommand CancelAddApplicationCommand { get; }
    public CreateApplicationRequest Request
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = new();
    public ImageSource? ApplicationIcon => Request.Icon?.Length > 0 ? _iconExtractor.DecodeFromPngBytes(Request.Icon) : null;

    private readonly INavigationService _navigationService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IIconExtractor _iconExtractor;
    private readonly IMediator _mediator;

    public AddCustomApplicationPageViewModel(INavigationService navigationService, IFileDialogService fileDialogService, IIconExtractor iconExtractor, IMediator mediator)
    {
        _navigationService = navigationService;
        _fileDialogService = fileDialogService;
        _iconExtractor = iconExtractor;
        _mediator = mediator;

        AddApplicationCommand = new RelayCommand(_ => AddApplication(), _ => CanAddApplication());
        CancelAddApplicationCommand = new RelayCommand(_ => CancelAddApplication());
        BrowseIconCommand = new RelayCommand(_ => BrowseIcon());
        BrowseApplicationCommand = new RelayCommand(_ => BrowseApplication());
    }

    public override void OnEnable()
    {
        Request = new CreateApplicationRequest();
        if (InitialPath is not null)
        {
            ApplyExecutable(InitialPath);
            InitialPath = null;
        }
        OnPropertyChanged(nameof(ApplicationIcon));
    }

    private void AddApplication()
    {
        _mediator.Send(Request);
        _navigationService.NavigateTo<HomeApplicationsPageViewModel>();
    }

    private bool CanAddApplication()
    {
        return !string.IsNullOrWhiteSpace(Request.Path) && !string.IsNullOrWhiteSpace(Request.Name);
    }

    private void CancelAddApplication()
    {
        _navigationService.NavigateTo<HomeApplicationsPageViewModel>();
    }

    private void BrowseIcon()
    {
        string? path = _fileDialogService.BrowseIcon();
        if (path is null)
            return;

        Request.Icon = File.ReadAllBytes(path);
        OnPropertyChanged(nameof(ApplicationIcon));
    }

    private void BrowseApplication()
    {
        string? path = _fileDialogService.BrowseApplication();
        if (path is not null)
            ApplyExecutable(path);
    }

    private void ApplyExecutable(string path)
    {
        Request.Path = path;
        Request.Name = Path.GetFileNameWithoutExtension(path);
        Request.Icon = _iconExtractor.ExtractPngBytes(path);
        OnPropertyChanged(nameof(Request));
        OnPropertyChanged(nameof(ApplicationIcon));
    }
}
