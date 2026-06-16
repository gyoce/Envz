namespace Envz.UI.ViewModels.Pages.Environments;

public class EnvironmentsPageViewModel : PageViewModel
{
    public override string Title => "Environments";
    public override Type? ParentPageType => null;
    public override Type RedirectType => typeof(HomeEnvironmentsPageViewModel);
}