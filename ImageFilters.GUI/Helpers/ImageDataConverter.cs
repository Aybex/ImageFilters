using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ImageFilters.GUI.Helpers
{
    internal class IntToImageWidthConverter : IValueConverter
    {
        public IntToImageWidthConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((ushort) value >0)
                return $"Width : {value} px";
            
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string width = (string)value;

            string numString = "";
            int val = 0;

            numString = width.Where(char.IsDigit).Aggregate(numString, (current, t) => current + t);

            if (numString.Length > 0)
                val = int.Parse(numString);

            return val;
        }
    }

    internal class IntToImageHeightConverter : IValueConverter
    {
        public IntToImageHeightConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ushort)value > 0)
                return $"Height : {value} px";

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string width = (string)value;

            string numString = "";
            int val = 0;

            numString = width.Where(char.IsDigit).Aggregate(numString, (current, t) => current + t);

            if (numString.Length > 0)
                val = int.Parse(numString);

            return val;
        }
    }
}
