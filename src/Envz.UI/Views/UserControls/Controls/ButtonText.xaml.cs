using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Envz.UI.Views.UserControls.Controls;

public partial class ButtonText : UserControl
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(ButtonText), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ButtonText));

    public static readonly DependencyProperty ButtonStyleProperty =
        DependencyProperty.Register(nameof(ButtonStyle), typeof(Style), typeof(ButtonText), new PropertyMetadata(System.Windows.Application.Current.FindResource("PrimaryButton")));

    public static readonly DependencyProperty TextStyleProperty =
        DependencyProperty.Register(nameof(TextStyle), typeof(Style), typeof(ButtonText), new PropertyMetadata(System.Windows.Application.Current.FindResource("TextBody")));

    public event RoutedEventHandler Click;

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public Style ButtonStyle
    {
        get => (Style)GetValue(ButtonStyleProperty);
        set => SetValue(ButtonStyleProperty, value);
    }

    public Style TextStyle
    {
        get => (Style)GetValue(TextStyleProperty);
        set => SetValue(TextStyleProperty, value);
    }

    public ButtonText()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Click?.Invoke(sender, e);
    }
}
