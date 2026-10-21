using System.Windows;

namespace Envz.Common.UI.Utils;

public static class InputState
{
    public static readonly DependencyProperty HasErrorProperty =
        DependencyProperty.RegisterAttached("HasError", typeof(bool), typeof(InputState), new PropertyMetadata(false));

    public static bool GetHasError(DependencyObject element) => (bool)element.GetValue(HasErrorProperty);
    public static void SetHasError(DependencyObject element, bool value) => element.SetValue(HasErrorProperty, value);
}