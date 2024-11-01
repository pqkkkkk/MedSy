using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Converter.Chat
{
    public class EmptyConnectingUsersConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int connectingUserCount)
                return connectingUserCount == 0 ? Visibility.Visible : Visibility.Collapsed;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
