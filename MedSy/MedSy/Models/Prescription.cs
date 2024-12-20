using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models;
public class Prescription : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public int prescriptionId { get; set; }
    public int totalPrice { get; set; }

    public DateOnly created_day { get; set; }
    public int consultationId { get; set; }
    public string status { get; set; }
}

