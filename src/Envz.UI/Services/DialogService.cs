using Envz.Common.Services.Dialogs;
using Envz.Common.ViewModels;
using Envz.UI.Views;
using Envz.UI.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Envz.UI.Services;

public class DialogService(IServiceProvider serviceProvider, ViewLocator viewLocator) : IDialogService
{
    private int _openDialogCount;

    public TViewModel ShowDialog<TViewModel>(Action<TViewModel>? configure = null)
        where TViewModel : DialogViewModelBase
    {
        TViewModel viewModel = serviceProvider.GetRequiredService<TViewModel>();
        configure?.Invoke(viewModel);

        MainDialogWindow window = new(viewLocator)
        {
            DataContext = viewModel,
            Owner = GetOwner()
        };

        viewModel.RequestClose += window.Close;
        _openDialogCount++;
        SetOverlayVisible(true);
        try
        {
            window.ShowDialog();
            return viewModel;
        }
        finally
        {
            viewModel.RequestClose -= window.Close;
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
