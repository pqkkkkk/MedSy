using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Windows.UI;

namespace MedSy.Converter.PrescriptionPayment
{
    public class StatusTextFgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color falseColor = ColorHelper.FromArgb(255, 229, 0, 0);
            Color trueColor = ColorHelper.FromArgb(255, 0, 204, 0);
            if (value is string status)
            {
                return (status == "paid") ? new SolidColorBrush(trueColor) : new SolidColorBrush(falseColor);
            }

            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
