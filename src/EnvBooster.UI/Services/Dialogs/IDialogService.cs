using System.Windows;

using EnvBooster.UI.ViewModels.Dialogs;

using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI.Services.Dialogs;

public interface IDialogService
{
    TResult? ShowDialog<TResult>(EDialogType dialogType);
}

public class DialogService(IServiceProvider serviceProvider) : IDialogService
{
    public TResult? ShowDialog<TResult>(EDialogType dialogType)
    {
        DialogViewModelBase<TResult> viewModel = (DialogViewModelBase<TResult>)serviceProvider.GetRequiredService(dialogType.ToDialogViewModelBaseType());
        Window window = CreateWindow(dialogType, viewModel);

        viewModel.RequestClose += result =>
        {
            window.DialogResult = result;
            window.Close();
        };

        bool? ok = window.ShowDialog();
        return ok == true ? viewModel.Result : default;
    }

    private Window CreateWindow<TResult>(EDialogType dialogType, DialogViewModelBase<TResult> viewModel)
    {
        Window windowDialog = (Window)serviceProvider.GetRequiredService(dialogType.ToDialogWindowType());
        windowDialog.DataContext = viewModel;
        windowDialog.Owner = System.Windows.Application.Current.MainWindow;
        return windowDialog;
    }
}