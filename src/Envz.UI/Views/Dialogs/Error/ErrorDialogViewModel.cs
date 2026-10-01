using Envz.Common.Utils;
using System.Windows.Input;

namespace Envz.UI.Views.Dialogs.Error;

public class ErrorDialogViewModel : DialogViewModelBase<bool>
{
    public string Message { get; set; } = string.Empty;
    public ICommand CloseCommand { get; }

    public ErrorDialogViewModel()
    {
        Title = "Error";
        CloseCommand = new RelayCommand(_ => Close(true, true));
    }
}