using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class PrescriptionDetail : INotifyPropertyChanged
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public string usage { get; set; }
        public int prescription_id { get; set; }
        public int price { get; set; }
        public int drug_id { get; set; }
        public Drug drug { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
