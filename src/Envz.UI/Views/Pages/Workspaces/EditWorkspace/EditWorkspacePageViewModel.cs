using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;

namespace Envz.UI.Views.Pages.Workspaces.EditWorkspace;

public class EditWorkspacePageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Workspaces;
    public override string Title => Strings.EditWorkspace_Title;
    public Workspace Workspace { get; set; } = null!;
}
