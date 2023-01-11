using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ImageFilters.GUI.Helpers;

[ValueConversion(typeof(bool), typeof(bool))]
internal class BoolInverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool booleanValue = (bool)value;
        return !booleanValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool booleanValue = (bool)value;
        return !booleanValue;
    }
}