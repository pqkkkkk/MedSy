using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class WorkScheduleDateItem : INotifyPropertyChanged
    {
        public string dateValue { get; set; }
        public bool isToday { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
