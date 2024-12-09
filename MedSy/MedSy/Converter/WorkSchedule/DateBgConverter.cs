using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace MedSy.Converter.WorkSchedule
{
    public class DateBgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color trueColor = ColorHelper.FromArgb(255, 217, 217, 217);
            Color falseColor = ColorHelper.FromArgb(255, 255, 255, 255);

            // Parameter sẽ chứa "i,j" (ví dụ: "2,3")
            if (value is ObservableCollection<Models.WorkScheduleDateItem> dateItems && parameter is string index)
            {
                int i = int.Parse(index);
                return dateItems[i-1].isToday == true ? new SolidColorBrush(trueColor) : new SolidColorBrush(falseColor);
                
            }
            return new SolidColorBrush(falseColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
