using System.Windows;
using System.Windows.Controls;

namespace Envz.UI.Views.Pages.Applications;

public partial class HomeApplicationsPage : UserControl
{
    public HomeApplicationsPage()
    {
        InitializeComponent();
        IsVisibleChanged += OnIsVisibleChanged;
    }

    private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if ((bool)e.NewValue)
            Dispatcher.BeginInvoke(() => SearchTextBox.Focus());
    }
}
