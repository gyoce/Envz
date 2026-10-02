using Envz.UI.Services;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Envz.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel, ViewLocator viewLocator)
    {
        InitializeComponent();
        PageHost.SetBinding(ContentProperty, new Binding(nameof(MainWindowViewModel.CurrentViewModel)) { Converter = viewLocator });
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