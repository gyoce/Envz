using System.Windows;
using System.Windows.Input;

namespace Envz.UI.Views.Dialogs;

public partial class SelectApplicationDialog : Window
{
    public SelectApplicationDialog()
    {
        InitializeComponent();
    }

    public void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void TitleBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}
