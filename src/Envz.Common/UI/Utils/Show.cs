using System.Windows;

namespace Envz.Common.UI.Utils;

public static class Show
{
    public static readonly DependencyProperty IfProperty =
        DependencyProperty.RegisterAttached("If", typeof(bool), typeof(Show), new PropertyMetadata(true, OnConditionChanged));

    public static readonly DependencyProperty UnlessProperty =
        DependencyProperty.RegisterAttached("Unless", typeof(bool), typeof(Show), new PropertyMetadata(false, OnConditionChanged));

    public static bool GetIf(DependencyObject element) => (bool)element.GetValue(IfProperty);
    public static void SetIf(DependencyObject element, bool value) => element.SetValue(IfProperty, value);

    public static bool GetUnless(DependencyObject element) => (bool)element.GetValue(UnlessProperty);
    public static void SetUnless(DependencyObject element, bool value) => element.SetValue(UnlessProperty, value);

    private static void OnConditionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element)
            element.Visibility = GetIf(d) && !GetUnless(d) ? Visibility.Visible : Visibility.Collapsed;
    }
}