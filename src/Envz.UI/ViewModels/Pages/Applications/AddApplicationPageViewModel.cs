using System.IO;
using System.Windows.Input;
using System.Windows.Media;

using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Services.Dialogs;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;

namespace Envz.UI.ViewModels.Pages.Applications;

public class AddApplicationPageViewModel : PageViewModel
{
    public override string Title => "Add application";
    public override Type ParentPageType => typeof(ApplicationsPageViewModel);
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
    public ImageSource? ApplicationIcon => Request.Icon.Length > 0 ? _iconExtractor.DecodeFromPngBytes(Request.Icon) : null;

    private readonly INavigationService _navigationService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IIconExtractor _iconExtractor;
    private readonly IMediator _mediator;

    public AddApplicationPageViewModel(INavigationService navigationService, IFileDialogService fileDialogService, IIconExtractor iconExtractor, IMediator mediator)
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
        string? path = _fileDialogService.OpenFile("Select an icon", "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*");
        if (path is null)
            return;

        Request.Icon = File.ReadAllBytes(path);
        OnPropertyChanged(nameof(ApplicationIcon));
    }

    private void BrowseApplication()
    {
        string? path = _fileDialogService.OpenFile("Select an application", "Executable files (*.exe)|*.exe|All files (*.*)|*.*");
        if (path is null)
            return;

        Request.Path = path;
        Request.Name = Path.GetFileNameWithoutExtension(path);
        Request.Icon = _iconExtractor.ExtractPngBytes(path);
        OnPropertyChanged(nameof(Request));
        OnPropertyChanged(nameof(ApplicationIcon));
    }
}
