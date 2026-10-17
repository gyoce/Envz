using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using Envz.UI.Views.Pages.Workspaces.EditWorkspace;
using System.Windows.Input;

namespace Envz.UI.Views.UserControls.WorkspaceItem;

public class WorkspaceItemViewModel(INavigationService navigationService, Workspace workspace) : ViewModelBase
{
    public ICommand EditWorkspaceCommand { get; } = new RelayCommand(_ => navigationService.NavigateTo<EditWorkspacePageViewModel>());
    public Workspace Workspace { get; } = workspace;
    public int NumberOfTools => Workspace.Tools.Count;
    public string NumberOfToolsText => NumberOfTools switch
    {
        0 => Strings.WorkspaceItem_ToolCountZero,
        1 => string.Format(Strings.WorkspaceItem_ToolCountOne, NumberOfTools),
        _ => string.Format(Strings.WorkspaceItem_ToolCountMany, NumberOfTools)
    };
}