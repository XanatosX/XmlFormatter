using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.Linq;

namespace XmlFormatterOsIndependent.Converters;
internal class UnderscoredTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string)
        {
            return null;
        }
        return $"_{value}";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string)
        {
            return null;
        }
        return value?.ToString()?.Skip(1).ToString();
    }
}
