using System.Windows;

using Envz.UI.ViewModels.Dialogs;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI.Services.Dialogs;

public interface IDialogService
{
    TResult? ShowDialog<TDialog, TViewModel, TResult>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase<TResult>
        where TDialog : Window;
}

public class DialogService(IServiceProvider serviceProvider) : IDialogService
{
    public TResult? ShowDialog<TDialog, TViewModel, TResult>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase<TResult>
        where TDialog : Window
    {
        DialogViewModelBase<TResult> viewModel = serviceProvider.GetRequiredService<TViewModel>();
        TDialog window = CreateWindow<TDialog, TResult>(viewModel);

        viewModel.RequestClose += result =>
        {
            window.DialogResult = result;
            window.Close();
        };

        bool? ok = window.ShowDialog();
        return ok == true ? viewModel.Result : default;
    }

    private TDialog CreateWindow<TDialog, TResult>(DialogViewModelBase<TResult> viewModel)
        where TDialog : Window
    {
        TDialog windowDialog = serviceProvider.GetRequiredService<TDialog>();
        windowDialog.DataContext = viewModel;
        windowDialog.Owner = System.Windows.Application.Current.MainWindow;
        return windowDialog;
    }
}