using System.Windows;
using System.Windows.Controls;

namespace Envz.UI.Views.Pages.Workspaces.HomeWorkspaces;

public partial class HomeWorkspacesPage : UserControl
{
    public HomeWorkspacesPage()
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
