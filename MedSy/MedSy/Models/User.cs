using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class User : INotifyPropertyChanged
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public DateTime birthday { get; set; }
        public string avatarPath { get; set; }
        public bool newMessage { get; set; }
        public string role { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
