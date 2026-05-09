using System.Collections.ObjectModel;
using System.Windows.Input;

using EnvBooster.Application.Environments;
using EnvBooster.UI.Utils;
using EnvBooster.UI.ViewModels.UserControls;
using EnvBooster.UI.Views;

namespace EnvBooster.UI.ViewModels.Pages.Environments;

public class CreateEnvironmentSubPageViewModel : ViewModelBase
{
    public ICommand CreateEnvironmentCommand { get; }
    public ICommand CancelCreateEnvironmentCommand { get; }
    public ICommand AddApplicationCommand { get; }
    public ObservableCollection<EnvironmentApplicationViewModel> EnvironmentApplications { get; } = [];
    public CreateEnvironmentRequest Request { get; set; } = new();
    public bool HasNoApplications => EnvironmentApplications.Count == 0;

    public CreateEnvironmentSubPageViewModel(CreateEnvironmentUseCase createEnvironmentUseCase)
    {
        EnvironmentApplications.CollectionChanged += (_, _) => OnPropertyChanged(nameof(HasNoApplications));
        CancelCreateEnvironmentCommand = new RelayCommand(_ =>
        {
            Request = new CreateEnvironmentRequest();
            Messenger.NotifyNavigationToHomeEnvironments();
        });
        CreateEnvironmentCommand = new RelayCommand(_ =>
        {
            createEnvironmentUseCase.Execute(Request);
            Messenger.NotifyEnvironmentCreated();
            Messenger.NotifyNavigationToHomeEnvironments();
        });
        AddApplicationCommand = new RelayCommand(_ =>
        {
            SelectApplicationDialog dialog = new()
            {
                Owner = System.Windows.Application.Current.MainWindow
            };
            dialog.ShowDialog();
        });
    }
}
