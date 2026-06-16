using System.Windows.Input;

using Envz.Functional.Environments;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Services.Navigation;
using Envz.UI.Utils;
using Envz.UI.ViewModels.UserControls;

namespace Envz.UI.ViewModels.Pages.Environments;

public class HomeEnvironmentsPageViewModel : PageViewModel
{
    public override string Title => "Home";
    public override Type ParentPageType => typeof(EnvironmentsPageViewModel);

    public ICommand NavigateToCreateEnvironmentCommand { get; }
    public SearchableCollection<EnvironmentViewModel, Environment> SearchableEnvironments { get; }

    private readonly IMediator _mediator;

    public HomeEnvironmentsPageViewModel(IMediator mediator, EnvironmentViewModelFactory environmentViewModelFactory, INavigationService navigationService)
    {
        _mediator = mediator;

        NavigateToCreateEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<CreateEnvironmentPageViewModel>());
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
