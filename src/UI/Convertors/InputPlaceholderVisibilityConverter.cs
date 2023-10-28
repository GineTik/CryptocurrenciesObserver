using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace UI.Convertors;

public class InputPlaceholderVisibilityConverter : MarkupExtension, IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value?.GetType() != typeof(int))
            throw new ArgumentException(
                $"LengthToVisibilityConverter accepts only {nameof(Int32)} type, " +
                $"but value have {value?.GetType().Name ?? "null"} type");

        return (int)value == 0 ? Visibility.Visible : Visibility.Collapsed;
    }
    
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {            
        return this;
    }
}
