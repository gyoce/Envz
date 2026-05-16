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

    private readonly IIconExtractor _iconExtractor;

    public AddApplicationSubPageViewModel([FromKeyedServices(ENavigationRegion.Applications)] INavigationService navigationService, IFileDialogService fileDialogService, IIconExtractor iconExtractor)
    {
        _iconExtractor = iconExtractor;
        CancelAddApplicationCommand = new RelayCommand(_ => navigationService.NavigateTo<HomeApplicationsSubPageViewModel>());
        BrowseApplicationCommand = new RelayCommand(_ =>
        {
            string? path = fileDialogService.OpenFile("Select an application", "Executable files (*.exe)|*.exe|All files (*.*)|*.*");

            if (path is not null)
            {
                Request.Path = path;
                Request.Name = Path.GetFileNameWithoutExtension(path);
                Request.Icon = iconExtractor.ExtractPngBytes(path);
                OnPropertyChanged(nameof(Request));
                OnPropertyChanged(nameof(ApplicationIcon));
            }
        });
    }
}