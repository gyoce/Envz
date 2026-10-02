using Envz.Common.Utils;
using Envz.Common.ViewModels;
using System.Windows.Input;

namespace Envz.Common.UI.Dialogs.Error;

public class ErrorDialogViewModel : DialogViewModelBase
{
    public string Message { get; set; } = string.Empty;
    public ICommand CloseCommand { get; }

    public ErrorDialogViewModel()
    {
        Title = "Error";
        CloseCommand = new RelayCommand(_ => Close());
    }
}