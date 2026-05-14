using System.Windows.Input;

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
    public string ApplicationPath
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = string.Empty;

    public AddApplicationSubPageViewModel([FromKeyedServices(ENavigationRegion.Applications)] INavigationService navigationService, IFileDialogService fileDialogService)
    {
        CancelAddApplicationCommand = new RelayCommand(_ => navigationService.NavigateTo<HomeApplicationsSubPageViewModel>());
        BrowseApplicationCommand = new RelayCommand(_ =>
        {
            string? path = fileDialogService.OpenFile("Select an application", "Executable files (*.exe)|*.exe|All files (*.*)|*.*");

            if (path is not null)
                ApplicationPath = path;
        });
    }
}