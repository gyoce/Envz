using System.Collections.ObjectModel;
using System.Windows.Input;

using EnvBooster.Application.Environments;
using EnvBooster.Domain.Entities;
using EnvBooster.UI.Services.Dialogs;
using EnvBooster.UI.Services.Navigation;
using EnvBooster.UI.Utils;
using EnvBooster.UI.ViewModels.Dialogs;
using EnvBooster.UI.ViewModels.UserControls;
using EnvBooster.UI.Views.Dialogs;

using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI.ViewModels.Pages.Environments;

public class CreateEnvironmentSubPageViewModel : ViewModelBase
{
    public ICommand CancelCreateEnvironmentCommand { get; }
    public ICommand CreateEnvironmentCommand { get; }
    public ICommand AddApplicationCommand { get; }
    public ObservableCollection<EnvironmentApplicationViewModel> EnvironmentApplications { get; } = [];
    public CreateEnvironmentRequest Request { get; set; } = new();
    public bool HasNoApplications => EnvironmentApplications.Count == 0;

    public CreateEnvironmentSubPageViewModel(CreateEnvironmentUseCase createEnvironmentUseCase, [FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService, IDialogService dialogService)
    {
        EnvironmentApplications.CollectionChanged += (_, _) => OnPropertyChanged(nameof(HasNoApplications));

        CreateEnvironmentCommand = new RelayCommand(_ =>
        {
            createEnvironmentUseCase.Execute(Request);
            navigationService.NavigateTo<HomeEnvironmentsSubPageViewModel>();
        });

        CancelCreateEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<HomeEnvironmentsSubPageViewModel>());

        AddApplicationCommand = new RelayCommand(_ =>
        {
            EnvironmentApplication? application = dialogService.ShowDialog<SelectApplicationDialog, SelectApplicationDialogViewModel, EnvironmentApplication>();
            System.Diagnostics.Debug.WriteLine($"Application : {application}");
        });
    }
}
