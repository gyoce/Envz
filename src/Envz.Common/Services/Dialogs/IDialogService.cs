using Envz.Common.ViewModels;
using System.Windows;

namespace Envz.Common.Services.Dialogs;

public interface IDialogService
{
    TResult? ShowDialog<TDialog, TViewModel, TResult>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase<TResult>
        where TDialog : Window;
}
