using System.Windows;
using System.Windows.Input;

using Envz.UI.ViewModels;

namespace Envz.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
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