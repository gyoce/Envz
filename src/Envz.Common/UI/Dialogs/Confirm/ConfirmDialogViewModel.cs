using Envz.Common.UI.Loc;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using System.Windows.Input;

namespace Envz.Common.UI.Dialogs.Confirm;

public class ConfirmDialogViewModel : DialogViewModelBase<bool>
{
    public string Message { get; set; } = string.Empty;
    public ICommand CloseAcceptCommand { get; }
    public ICommand CloseDeclineCommand { get; }
    public string TextAccept { get; set; } = Strings.Global_Yes;
    public string TextDecline { get; set; } = Strings.Global_No;

    public ConfirmDialogViewModel()
    {
        CloseAcceptCommand = new RelayCommand(_ => Close(true));
        CloseDeclineCommand = new RelayCommand(_ => Close(false));
    }
}