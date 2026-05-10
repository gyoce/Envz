namespace Envz.UI.ViewModels.Pages.Applications;

public class HomeApplicationsSubPageViewModel : ViewModelBase
{
    public string SearchText
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = string.Empty;
}