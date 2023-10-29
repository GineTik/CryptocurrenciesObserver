using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace UI.Convertors;

public enum NullConvertEnum
{
    ToVisibility,
    ToCollapsed
}

public class NullVisibilityConverter : MarkupExtension, IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var convertTo = (NullConvertEnum)(parameter ?? throw new ArgumentNullException(nameof(parameter)));

        return convertTo switch
        {
            NullConvertEnum.ToVisibility => value == null ? Visibility.Visible : Visibility.Collapsed,
            NullConvertEnum.ToCollapsed => value == null ? Visibility.Collapsed : Visibility.Visible,
            _ => throw new ArgumentOutOfRangeException()
        };
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
