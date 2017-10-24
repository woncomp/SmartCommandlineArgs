using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCmdArgs.Helper
{
    class LamdaConverter : ConverterBase
    {
        private Func<object, object, object> converter;
        private Func<object, object, object> reverseConverter;

        public LamdaConverter(Func<object, object, object> converter, Func<object, object, object> reverseConverter = null)
        {
            this.converter = converter;
            this.reverseConverter = reverseConverter;
        } 

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return converter(value, parameter);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return reverseConverter(value, parameter);
        }
    }
}
