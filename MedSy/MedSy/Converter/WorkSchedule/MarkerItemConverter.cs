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
    public class MarkerItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color trueColor = ColorHelper.FromArgb(255, 51, 102, 153);
            Color falseColor = ColorHelper.FromArgb(255, 255, 255, 255);

            // Parameter sẽ chứa "i,j" (ví dụ: "2,3")
            if (value is ObservableCollection<ObservableCollection<Models.WorkScheduleMarkerItem>> marker && parameter is string indices)
            {
                var parts = indices.Split(',');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int row) &&
                    int.TryParse(parts[1], out int col) &&
                    row < marker.Count &&
                    col < marker[row].Count)
                {
                    return marker[row][col].isMarked == true ? new SolidColorBrush(trueColor) : new SolidColorBrush(falseColor);
                }
            }
            return new SolidColorBrush(falseColor); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
