using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Envz.Common.UI.Controls;

public enum EImagePlacement
{
    Left,
    Right
}

public partial class ButtonTextImage : UserControl
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(ButtonTextImage), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(ButtonTextImage));

    public static readonly DependencyProperty ImagePlacementProperty =
        DependencyProperty.Register(nameof(ImagePlacement), typeof(EImagePlacement), typeof(ButtonTextImage), new PropertyMetadata(EImagePlacement.Left));

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ButtonTextImage));

    public static readonly DependencyProperty ButtonStyleProperty =
        DependencyProperty.Register(nameof(ButtonStyle), typeof(Style), typeof(ButtonTextImage), new PropertyMetadata(System.Windows.Application.Current.FindResource("PrimaryButton")));

    public static readonly DependencyProperty TextStyleProperty =
        DependencyProperty.Register(nameof(TextStyle), typeof(Style), typeof(ButtonTextImage), new PropertyMetadata(System.Windows.Application.Current.FindResource("TextBody")));

    public static readonly DependencyProperty ImageWidthProperty =
        DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(ButtonTextImage), new PropertyMetadata(16d));

    public static readonly DependencyProperty ImageHeightProperty =
        DependencyProperty.Register(nameof(ImageHeight), typeof(double), typeof(ButtonTextImage), new PropertyMetadata(16d));

    public event RoutedEventHandler? Click;

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ImageSource Source
    {
        get => (ImageSource)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public EImagePlacement ImagePlacement
    {
        get => (EImagePlacement)GetValue(ImagePlacementProperty);
        set => SetValue(ImagePlacementProperty, value);
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

    public double ImageWidth
    {
        get => (double)GetValue(ImageWidthProperty);
        set => SetValue(ImageWidthProperty, value);
    }

    public double ImageHeight
    {
        get => (double)GetValue(ImageHeightProperty);
        set => SetValue(ImageHeightProperty, value);
    }

    public ButtonTextImage()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Click?.Invoke(sender, e);
    }
}
