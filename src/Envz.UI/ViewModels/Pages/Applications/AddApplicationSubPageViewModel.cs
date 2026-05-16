using System.IO;
using System.Windows.Input;
using System.Windows.Media;

using Envz.Application.Applications;
using Envz.UI.Services;
using Envz.UI.Services.Dialogs;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Applications;

public class AddApplicationSubPageViewModel : ViewModelBase
{
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

    public AddApplicationSubPageViewModel([FromKeyedServices(ENavigationRegion.Applications)] INavigationService navigationService, IFileDialogService fileDialogService, IIconExtractor iconExtractor)
    {
        _navigationService = navigationService;
        _fileDialogService = fileDialogService;
        _iconExtractor = iconExtractor;

        AddApplicationCommand = new RelayCommand(_ => { }, _ => CanAddApplication());
        CancelAddApplicationCommand = new RelayCommand(_ => CancelAddApplication());
        BrowseIconCommand = new RelayCommand(_ => BrowseIcon());
        BrowseApplicationCommand = new RelayCommand(_ => BrowseApplication());
    }

    private bool CanAddApplication()
    {
        return !string.IsNullOrWhiteSpace(Request.Path) && !string.IsNullOrWhiteSpace(Request.Name);
    }

    private void CancelAddApplication()
    {
        _navigationService.NavigateTo<HomeApplicationsSubPageViewModel>();
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
