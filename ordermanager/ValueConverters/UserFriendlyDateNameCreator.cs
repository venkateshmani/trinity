using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ordermanager.ValueConverters
{
    public class UserFriendlyDateNameCreator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                try
                {
                    DateTime dt = DateTime.Parse((string)value);
                    if (dt.ToShortDateString() == DateTime.Now.ToShortDateString())
                    {
                        return "Today";
                    }
                }
                catch
                {
                     //Keep Quite                            
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
