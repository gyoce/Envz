using Envz.UI.Views.Pages.Environments.HomeEnvironments;

namespace Envz.UI.Views.Pages.Environments;

public class EnvironmentsPageViewModel : PageViewModel
{
    public override string Title => "Environments";
    public override Type? ParentPageType => null;
    public override Type RedirectType => typeof(HomeEnvironmentsPageViewModel);
}