using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace MedSy.Converter.ConsultationRequest
{
    class selectedStatusBgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color trueColor = ColorHelper.FromArgb(255, 193, 193,193);
            Color falseColor = ColorHelper.FromArgb(255, 255,255,255);

            if (value is string selectedSatus && parameter is string actualStatus)
            {
                return selectedSatus == actualStatus ? new SolidColorBrush(trueColor) : new SolidColorBrush(falseColor);
            }

            return new SolidColorBrush(falseColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
