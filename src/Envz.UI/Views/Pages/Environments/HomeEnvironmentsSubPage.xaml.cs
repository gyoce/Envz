using System.Windows;
using System.Windows.Controls;

namespace Envz.UI.Views.Pages.Environments;

public partial class HomeEnvironmentsSubPage : UserControl
{
    public HomeEnvironmentsSubPage()
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
