using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace MedSy.Converter.Main
{
    public class selectedNavigationItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color trueColor = ColorHelper.FromArgb(255, 51, 102, 153);
            Color falseColor = ColorHelper.FromArgb(255, 102,102, 102);

            if (value is string selectedPageName && parameter is string actualPageName)
            {
                return selectedPageName == actualPageName ? new SolidColorBrush(trueColor) : new SolidColorBrush(falseColor);
            }

            return new SolidColorBrush(falseColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
