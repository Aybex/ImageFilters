using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ImageFilters.GUI.Helpers;

[ValueConversion(typeof(bool), typeof(bool))]
internal class BoolInverter : IValueConverter
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

[ValueConversion(typeof(bool), typeof(Visibility))]
internal class BoolToVisibilityInversed : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		bool booleanValue = (bool)value;

		return (bool)value ? Visibility.Hidden : Visibility.Visible;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		var visibility = (Visibility)value;

		return visibility switch
		{
			Visibility.Visible => false,
			_ => true
		};
	}
}