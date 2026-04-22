using System.Windows;
using System.Windows.Input;

namespace EnvBooster.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MinimizeButtonClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void TitleBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}