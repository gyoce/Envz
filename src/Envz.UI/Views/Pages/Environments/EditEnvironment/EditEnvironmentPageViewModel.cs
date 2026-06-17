namespace Envz.UI.Views.Pages.Environments.EditEnvironment;

public class EditEnvironmentPageViewModel : PageViewModel
{
    public override string Title => "Edit environment";
    public override Type ParentPageType => typeof(EnvironmentsPageViewModel);
    public Environment Environment { get; set; } = null!;
}
