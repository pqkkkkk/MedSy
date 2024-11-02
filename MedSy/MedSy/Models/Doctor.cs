using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    {
        public string DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string Specialty { get; set; }
        public string Gender { get; set; }
        public int ExperienceYear { get; set; }
        public string Email{ get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
       
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
