using System.Windows;
using System.Windows.Controls;

namespace Envz.UI.Views.Pages.Environments.SelectApplication;

public partial class SelectApplicationPage : UserControl
{
    public SelectApplicationPage()
    {
        InitializeComponent();
        IsVisibleChanged += OnIsVisibleChanged;
    }

    private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if ((bool)e.NewValue)
            Dispatcher.BeginInvoke(() => SearchApplicationTextBox.Focus());
    }
}
