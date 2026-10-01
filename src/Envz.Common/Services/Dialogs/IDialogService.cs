using Envz.Common.ViewModels;

namespace Envz.Common.Services.Dialogs;

public interface IDialogService
{
    TResult? ShowDialog<TViewModel, TResult>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase<TResult>;
}
