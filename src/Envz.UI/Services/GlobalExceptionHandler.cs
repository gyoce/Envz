using Envz.Common.Services.Dialogs;
using Envz.Common.UI.Dialogs.Error;
using Envz.Common.UI.Loc;
using Envz.Domain.Exceptions;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace Envz.UI.Services;

public class GlobalExceptionHandler(IDialogService dialogService)
{
    private bool _isShowing;

    public void Register(System.Windows.Application application)
    {
        application.DispatcherUnhandledException += OnDispatcherUnhandledException;
        TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        e.Handled = true;
        Show(e.Exception);
    }

    private void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        e.SetObserved();
        System.Windows.Application.Current?.Dispatcher.BeginInvoke(() => Show(e.Exception.GetBaseException()));
    }

    private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        MessageBox.Show(e.ExceptionObject.ToString(), "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void Show(Exception exception)
    {
        (string title, string message) = ExtractInfoFromException(exception);

        if (_isShowing)
        {
            Debug.WriteLine(exception);
            return;
        }

        _isShowing = true;
        try
        {
            Window? mainWindow = System.Windows.Application.Current.MainWindow;
            if (mainWindow is { IsLoaded: true })
            {
                dialogService.ShowDialog<ErrorDialogViewModel>(vm =>
                {
                    vm.Title = title;
                    vm.Message = message;
                });
            }
            else
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown(1);
            }
        }
        catch (Exception dialogException)
        {
            MessageBox.Show(dialogException.ToString(), title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            _isShowing = false;
        }
    }

    private static (string title, string message) ExtractInfoFromException(Exception exception)
    {
        return exception switch
        {
            EnvzException e => (Strings.KnownError_Title, string.Format(Localizer.Instance[$"KnownError_{e.Code}"], e.Args)),
            _ => (Strings.UnknownError_Title, Strings.UnknownError_Message)
        };
    }
}