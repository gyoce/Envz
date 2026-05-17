using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Envz.UI.Views.UserControls.Controls;

public partial class Breadcrumb : UserControl
{
    public static readonly DependencyProperty PrimaryMenuCommandProperty =
        DependencyProperty.Register(nameof(PrimaryMenuCommand), typeof(ICommand), typeof(Breadcrumb));

    public static readonly DependencyProperty PrimaryMenuTextProperty =
        DependencyProperty.Register(nameof(PrimaryMenuText), typeof(string), typeof(Breadcrumb));

    public static readonly DependencyProperty SecondaryMenuTextProperty =
        DependencyProperty.Register(nameof(SecondaryMenuText), typeof(string), typeof(Breadcrumb));

    public ICommand PrimaryMenuCommand
    {
        get => (ICommand)GetValue(PrimaryMenuCommandProperty);
        set => SetValue(PrimaryMenuCommandProperty, value);
    }

    public string PrimaryMenuText
    {
        get => (string)GetValue(PrimaryMenuTextProperty);
        set => SetValue(PrimaryMenuTextProperty, value);
    }

    public string SecondaryMenuText
    {
        get => (string)GetValue(SecondaryMenuTextProperty);
        set => SetValue(SecondaryMenuTextProperty, value);
    }

    public Breadcrumb()
    {
        InitializeComponent();
    }
}
