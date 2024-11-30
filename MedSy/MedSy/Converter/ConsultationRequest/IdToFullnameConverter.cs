using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Converter.ConsultationRequest
{
    public class IdToFullnameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
          
            if (value != null && value is int userId)
            {
                return (Application.Current as App).locator.userDao.getUserById(userId).fullName;
            }

            return "None";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
