using System.Windows.Input;

using Envz.UI.Services.Navigation;
using Envz.UI.Utils;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Applications;

public class HomeApplicationsSubPageViewModel : ViewModelBase
{
    public ICommand AddApplicationCommand { get; }
    public string SearchText
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = string.Empty;

    public HomeApplicationsSubPageViewModel([FromKeyedServices(ENavigationRegion.Applications)] INavigationService navigationService)
    {
        AddApplicationCommand = new RelayCommand(_ => navigationService.NavigateTo<AddApplicationSubPageViewModel>());
    }
}