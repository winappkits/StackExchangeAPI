using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace APIMASH_StackExchange_StarterKit.Converters
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                string formatterString = parameter.ToString();

                if (!String.IsNullOrEmpty(formatterString))
                {
                    return String.Format(culture, formatterString, value);
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ToLowercaseConverter : IValueConverter
    {
        private static readonly StringFormatConverter _stringFormatConverter = new StringFormatConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _stringFormatConverter.Convert(value, targetType, parameter, culture).ToString().ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class ToUppercaseConverter : IValueConverter
    {
        private static readonly StringFormatConverter _stringFormatConverter = new StringFormatConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return _stringFormatConverter.Convert(value, targetType, parameter, culture).ToString().ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
