using Envz.Application.Mediator;
using Envz.Application.Requests.Tools;
using Envz.Common.Services.Dialogs;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Dialogs.Confirm;
using Envz.Common.UI.Loc;
using Envz.Common.UI.Utils;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using Envz.UI.Services;
using Envz.UI.Views.Pages.Tools.AddTool;
using Envz.UI.Views.UserControls.ToolItem;
using System.Windows.Input;

namespace Envz.UI.Views.Pages.Tools.HomeTools;

public class HomeToolsPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Tools;
    public override string Title => Strings.HomeTools_Title;

    public ICommand AddToolCommand { get; }
    public SearchableCollection<ToolItemViewModel, Tool> SearchableTools { get; }

    private readonly IMediator _mediator;
    private readonly IDialogService _dialogService;

    public HomeToolsPageViewModel(INavigationService navigationService, IMediator mediator, ToolItemViewModelFactory viewModelFactory, IDialogService dialogService)
    {
        _mediator = mediator;
        _dialogService = dialogService;
        AddToolCommand = new RelayCommand(_ => navigationService.NavigateTo<AddToolPageViewModel>());
        SearchableTools = new SearchableCollection<ToolItemViewModel, Tool>(
            tool => tool.Name,
            tool => viewModelFactory.Create(tool, DeleteTool)
        );

        LoadTools();
    }

    private void DeleteTool(Tool tool)
    {
        bool confirmed = _dialogService.ShowDialog<ConfirmDialogViewModel>(viewModel =>
        {
            viewModel.Message = string.Format(Strings.ConfirmDialog_DeleteToolMessage, tool.Name);
            viewModel.Title = Strings.ConfirmDialog_DeleteToolTitle;
        }).Result;

        if (!confirmed)
            return;

        _mediator.Send(new DeleteToolRequest
        {
            ToolName = tool.Name
        });
        LoadTools();
    }

    public override void OnEnable()
    {
        LoadTools();
    }

    private void LoadTools()
    {
        SearchableTools.UnfilteredItems = _mediator.Send(new GetToolsRequest());
    }
}