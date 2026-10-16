using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Envz.Common.UI.Controls;

public partial class SearchBox : UserControl
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(SearchBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(SearchBox), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty FocusOnShowProperty =
        DependencyProperty.Register(nameof(FocusOnShow), typeof(bool), typeof(SearchBox), new PropertyMetadata(true));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public bool FocusOnShow
    {
        get => (bool)GetValue(FocusOnShowProperty);
        set => SetValue(FocusOnShowProperty, value);
    }

    public SearchBox()
    {
        InitializeComponent();
        IsVisibleChanged += OnIsVisibleChanged;
    }

    private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (FocusOnShow && (bool)e.NewValue)
            Dispatcher.BeginInvoke(() => InputTextBox.Focus());
    }

    private void InputTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Escape || string.IsNullOrEmpty(InputTextBox.Text))
            return;

        InputTextBox.Clear();
        e.Handled = true;
    }
}