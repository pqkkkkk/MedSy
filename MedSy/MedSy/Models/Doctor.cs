using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class Doctor : INotifyPropertyChanged
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string avatarPath { get; set; }
        public bool newMessage { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
