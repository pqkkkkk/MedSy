using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class PatientManagementItem : INotifyPropertyChanged
    {
        public Models.User patient { get; set; }
        public ObservableCollection<Models.Consultation> doneConsultations { get; set; }
        public ObservableCollection<Models.Consultation> todayConsultations { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;
    }
}
