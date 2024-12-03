﻿using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Converter.ConsultationRequest
{
    public class AcceptedCRNotiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int newCR)
            {
                return newCR == 0 ? "There is no notification" : "Has a new consultation request which is accepted from doctor. Refresh to update";
            }
            return "There is no new consultation request";
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
