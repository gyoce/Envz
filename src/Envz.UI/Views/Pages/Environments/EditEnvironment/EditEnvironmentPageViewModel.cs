using Envz.Common.Services.Navigation;

namespace Envz.UI.Views.Pages.Environments.EditEnvironment;

public class EditEnvironmentPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Environments;
    public override string Title => "Edit environment";
    public Environment Environment { get; set; } = null!;
}
