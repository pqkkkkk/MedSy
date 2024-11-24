using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

namespace MedSy.Models
{
    public class Consultation : INotifyPropertyChanged
    {
        public int id { get; set; }
        public string status { get; set; }

        public string form { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public string result { get; set; }
        public string reason { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly startTime { get; set; }
        public TimeOnly endTime { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        
    }
    
}
