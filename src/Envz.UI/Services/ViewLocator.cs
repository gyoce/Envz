using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Envz.UI.Services;

public class ViewLocator(IServiceProvider serviceProvider) : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return null;

        FrameworkElement view = serviceProvider.GetRequiredKeyedService<FrameworkElement>(value.GetType());
        view.DataContext = value;
        return view;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}