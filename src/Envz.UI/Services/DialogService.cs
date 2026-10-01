using Envz.Common.Services.Dialogs;
using Envz.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Envz.UI.Views.Dialogs;

namespace Envz.UI.Services;

public class DialogService(IServiceProvider serviceProvider) : IDialogService
{
    private int _openDialogCount;

    public TResult? ShowDialog<TViewModel, TResult>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase<TResult>
    {
        TViewModel viewModel = serviceProvider.GetRequiredService<TViewModel>();
        configure?.Invoke(viewModel);

        MainDialogWindow window = new()
        {
            DataContext = viewModel,
            Owner = GetOwner()
        };

        void OnRequestClose(bool? result)
        {
            window.DialogResult = result;
        }

        viewModel.RequestClose += OnRequestClose;
        _openDialogCount++;
        SetOverlayVisible(true);
        try
        {
            bool? ok = window.ShowDialog();
            return ok == true ? viewModel.Result : default;
        }
        finally
        {
            viewModel.RequestClose -= OnRequestClose;
            _openDialogCount--;
            SetOverlayVisible(_openDialogCount > 0);
        }
    }

    private static Window? GetOwner()
    {
        System.Windows.Application app = System.Windows.Application.Current;
        return app.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive) ?? app.MainWindow;
    }

    private static void SetOverlayVisible(bool isVisible)
    {
        if (System.Windows.Application.Current.MainWindow?.DataContext is MainWindowViewModel mainVm)
            mainVm.IsDialogOpen = isVisible;
    }
}
