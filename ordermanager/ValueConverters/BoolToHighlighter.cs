using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ordermanager.ValueConverters
{
    class BoolToHighlighter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                bool bValue = (bool)value;
                if (bValue)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 220, 0, 12));
                }
            }

          return new SolidColorBrush(Colors.Black);      
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
