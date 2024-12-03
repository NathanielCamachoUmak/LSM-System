using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LSM_prototype.Core
{
    public class ColumnWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double actualWidth && parameter is string proportion)
            {
                var fractions = proportion.Split('/');
                if (fractions.Length == 2 &&
                    double.TryParse(fractions[0], out var numerator) &&
                    double.TryParse(fractions[1], out var denominator) &&
                    denominator != 0)
                {
                    return actualWidth * (numerator / denominator);
                }
            }
            return value; // Fallback to the original width if parsing fails
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
