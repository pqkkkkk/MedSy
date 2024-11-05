using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class Management
    {
        public int id { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public bool doctorNewMessage { get; set; }
        public bool patientNewMessage { get; set; }
    }
}
