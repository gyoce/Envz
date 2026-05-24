using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Envz.UI.Views.UserControls.Controls;

public partial class Breadcrumb : UserControl
{
    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(nameof(Items), typeof(IEnumerable), typeof(Breadcrumb));


    public IEnumerable? Items
    {
        get => (IEnumerable?)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public Breadcrumb()
    {
        InitializeComponent();
    }
}
