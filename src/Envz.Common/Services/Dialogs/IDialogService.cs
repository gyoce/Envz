using Envz.Common.ViewModels;

namespace Envz.Common.Services.Dialogs;

public interface IDialogService
{
    TViewModel ShowDialog<TViewModel>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase;
}
