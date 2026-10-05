using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.ViewModels;

namespace Envz.UI.Views.Pages.Environments.EditEnvironment;

public class EditEnvironmentPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Environments;
    public override string Title => Strings.EditEnvironment_Title;
    public Environment Environment { get; set; } = null!;
}
