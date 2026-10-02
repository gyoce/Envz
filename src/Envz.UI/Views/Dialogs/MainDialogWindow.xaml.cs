using Envz.UI.Services;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Envz.UI.Views.Dialogs;

public partial class MainDialogWindow : Window
{
    public MainDialogWindow(ViewLocator viewLocator)
    {
        InitializeComponent();
        DialogHost.SetBinding(ContentProperty, new Binding { Converter = viewLocator });
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.Escape && DataContext is IDialogViewModel { IsCancellable: true })
        {
            Close();
            e.Handled = true;
        }
        base.OnPreviewKeyDown(e);
    }

    private void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
