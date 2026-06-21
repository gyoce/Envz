using System.Windows;

using Envz.UI.Views.Dialogs;

namespace Envz.UI.Services.Dialogs;

public interface IDialogService
{
    TResult? ShowDialog<TDialog, TViewModel, TResult>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase<TResult>
        where TDialog : Window;
}
