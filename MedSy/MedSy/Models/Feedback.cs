using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class Feedback:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public string Content {  get; set; }
        public double Rating {  get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
