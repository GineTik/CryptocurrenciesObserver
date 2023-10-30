using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UI.Convertors;

public class PriceToStringConverter : MarkupExtension, IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(value);

        var price = (decimal)value;

        return $"${price:0.00}";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
