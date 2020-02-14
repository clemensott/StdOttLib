using System;
using Windows.UI.Xaml.Data;

namespace StdOttUwp.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return StdOttStandard.StdUtils.ToString((TimeSpan)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return TimeSpan.Parse((string)value);
        }
    }
}
