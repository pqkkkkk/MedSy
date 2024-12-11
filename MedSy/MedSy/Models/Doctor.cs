using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class Doctor : User
    {
        public string speciality { get; set; }
        public int experienceYear { get; set; }
        public int consultation_price { get; set; }
        public float rating { get; set; }


    }
}
