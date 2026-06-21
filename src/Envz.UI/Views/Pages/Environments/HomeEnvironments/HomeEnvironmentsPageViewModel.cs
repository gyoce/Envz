using System.Windows.Input;
using Envz.Common.Services.Navigation;
using Envz.Functional.Environments;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Utils;
using Envz.UI.Views.Pages.Environments.CreateEnvironment;
using Envz.UI.Views.UserControls.EnvironmentItem;

namespace Envz.UI.Views.Pages.Environments.HomeEnvironments;

public class HomeEnvironmentsPageViewModel : PageViewModel
{
    public override string Title => "Home";
    public override ENavigationCategory Category => ENavigationCategory.Environments;

    public ICommand NavigateToCreateEnvironmentCommand { get; }
    public SearchableCollection<EnvironmentItemViewModel, Environment> SearchableEnvironments { get; }

    private readonly IMediator _mediator;

    public HomeEnvironmentsPageViewModel(IMediator mediator, ViewModelFactory viewModelFactory, INavigationService navigationService)
    {
        _mediator = mediator;

        NavigateToCreateEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<CreateEnvironmentPageViewModel>());
        SearchableEnvironments = new SearchableCollection<EnvironmentItemViewModel, Environment>(
            env => env.Name, 
            env => viewModelFactory.Create<EnvironmentItemViewModel>(env)
        );

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
