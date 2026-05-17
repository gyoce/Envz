using System.Windows.Input;

using Envz.Functional.Environments;
using Envz.Functional.Mediator;
using Envz.Domain.Entities;
using Envz.UI.Services.Dialogs;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.Dialogs;
using Envz.UI.Views.Dialogs;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Environments;

public class CreateEnvironmentSubPageViewModel : ViewModelBase
{
    public override string Title => "Create environment";
    public ICommand CancelCreateEnvironmentCommand { get; }
    public ICommand CreateEnvironmentCommand { get; }
    public ICommand AddApplicationCommand { get; }
    public CreateEnvironmentRequest Request { get; set; } = new();

    public CreateEnvironmentSubPageViewModel(IMediator mediator, [FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService, IDialogService dialogService)
    {

        CreateEnvironmentCommand = new RelayCommand(_ =>
        {
            mediator.Send(Request);
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
