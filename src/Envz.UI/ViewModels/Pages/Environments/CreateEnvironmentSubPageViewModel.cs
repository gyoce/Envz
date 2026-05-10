using System.Collections.ObjectModel;
using System.Windows.Input;

using Envz.Application.Environments;
using Envz.Domain.Entities;
using Envz.UI.Services.Dialogs;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.Dialogs;
using Envz.UI.ViewModels.UserControls;
using Envz.UI.Views.Dialogs;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Environments;

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
