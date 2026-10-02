using Envz.Common.ViewModels;

namespace Envz.Common.Services.Dialogs;

public interface IDialogService
{
    void ShowDialog<TViewModel>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase;

    TResult? ShowDialog<TViewModel, TResult>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase<TResult>;
}
