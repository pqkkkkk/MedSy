using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class WorkScheduleMarkerItem : INotifyPropertyChanged
    {
        public bool isMarked { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
