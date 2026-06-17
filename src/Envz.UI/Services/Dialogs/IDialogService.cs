using System.Windows;

using Envz.UI.Views;
using Envz.UI.Views.Dialogs;

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

        MainWindowViewModel? mainVm = System.Windows.Application.Current.MainWindow?.DataContext as MainWindowViewModel;
        mainVm?.IsDialogOpen = true;
        bool? ok = window.ShowDialog();
        mainVm?.IsDialogOpen = false;
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