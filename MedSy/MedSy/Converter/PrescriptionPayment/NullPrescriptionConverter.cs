using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Windows.UI;
using Microsoft.UI.Xaml;

namespace MedSy.Converter.PrescriptionPayment
{
    public class NullPrescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value == null)
            {
                return Visibility.Visible;
            }
            if (value is Models.Prescription selectedPrescription)
            {
                return (selectedPrescription.totalPrice == 0) ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
