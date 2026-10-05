using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.UI.Utils;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Functional.Environments;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Views.Pages.Environments.CreateEnvironment;
using Envz.UI.Views.UserControls.EnvironmentItem;
using System.Windows.Input;

namespace Envz.UI.Views.Pages.Environments.HomeEnvironments;

public class HomeEnvironmentsPageViewModel : PageViewModel
{
    public override string Title => Strings.HomeEnvironments_Title;
    public override ENavigationCategory Category => ENavigationCategory.Environments;

    public ICommand NavigateToCreateEnvironmentCommand { get; }
    public SearchableCollection<EnvironmentItemViewModel, Environment> SearchableEnvironments { get; }

    private readonly IMediator _mediator;

    public HomeEnvironmentsPageViewModel(IMediator mediator, EnvironmentItemViewModelFactory viewModelFactory, INavigationService navigationService)
    {
        _mediator = mediator;

        NavigateToCreateEnvironmentCommand = new RelayCommand(_ => navigationService.NavigateTo<CreateEnvironmentPageViewModel>());
        SearchableEnvironments = new SearchableCollection<EnvironmentItemViewModel, Environment>(
            env => env.Name,
            env => viewModelFactory.Create(env)
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
