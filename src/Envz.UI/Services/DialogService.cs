using Envz.Common.Services.Dialogs;
using Envz.UI.Views;
using Envz.UI.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Envz.UI.Services;

public class DialogService(IServiceProvider serviceProvider, ViewLocator viewLocator) : IDialogService
{
    private int _openDialogCount;

    public void ShowDialog<TViewModel>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase
    {
        Show(configure);
    }

    public TResult? ShowDialog<TViewModel, TResult>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase<TResult>
    {
        (bool ok, TViewModel viewModel) = Show(configure);
        return ok ? viewModel.Result : default;
    }

    private (bool Ok, TViewModel ViewModel) Show<TViewModel>(Action<TViewModel>? configure)
        where TViewModel : DialogViewModelBase
    {
        TViewModel viewModel = serviceProvider.GetRequiredService<TViewModel>();
        configure?.Invoke(viewModel);

        MainDialogWindow window = new(viewLocator)
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
            return (window.ShowDialog() == true, viewModel);
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
