using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class Message : INotifyPropertyChanged
    {
        public string content { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
