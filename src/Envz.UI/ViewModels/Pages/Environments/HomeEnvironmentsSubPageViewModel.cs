using System.Windows.Input;

using Envz.Functional.Environments;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.UserControls;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.ViewModels.Pages.Environments;

public class HomeEnvironmentsSubPageViewModel : ViewModelBase
{
    public override string Title => "Home";
    public ICommand NavigateToCreateEnvironmentCommand { get; }
    public SearchableCollection<EnvironmentViewModel, Environment> SearchableEnvironments { get; }

    private readonly IMediator _mediator;

    public HomeEnvironmentsSubPageViewModel(IMediator mediator, EnvironmentViewModelFactory environmentViewModelFactory, [FromKeyedServices(ENavigationRegion.Environments)] INavigationService navigationService)
    {
        _mediator = mediator;
        NavigateToCreateEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<CreateEnvironmentSubPageViewModel>());
        SearchableEnvironments = new SearchableCollection<EnvironmentViewModel, Environment>(env => env.Name, environmentViewModelFactory.Create);
        LoadEnvironments();
    }

    public override void OnEnable()
    {
        LoadEnvironments();
    }

    private void LoadEnvironments()
    {
        SearchableEnvironments.UnfilteredItems = _mediator.Send(new GetEnvironmentsRequest());
    }
}
