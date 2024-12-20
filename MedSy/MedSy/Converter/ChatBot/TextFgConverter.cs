using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Windows.UI;

namespace MedSy.Converter.ChatBot
{
    public class TextFgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color bgColor = ColorHelper.FromArgb(255, 51, 102, 153);
            if (value is int senderId)
            {
                return (senderId == -1) ? new SolidColorBrush(bgColor) : new SolidColorBrush(Colors.White);
            }

            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
