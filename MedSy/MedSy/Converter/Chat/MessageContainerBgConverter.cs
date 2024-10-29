using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;
using Windows.UI;

namespace MedSy.Converter.Chat
{
    public class MessageContainerBgConverter : IValueConverter
    {
        
        
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int userId = (Application.Current as App).locator.patientDao.getPatient().id;
            Color bgColor = ColorHelper.FromArgb(255,167,237,231);
            if (value is int senderId)
            {
                return (senderId == userId) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(bgColor);
            }

            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
