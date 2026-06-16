namespace Envz.UI.ViewModels.Pages.Applications;

public class ApplicationsPageViewModel : PageViewModel
{
    public override string Title => "Applications";
    public override Type? ParentPageType => null;
    public override Type RedirectType => typeof(HomeApplicationsPageViewModel);
}