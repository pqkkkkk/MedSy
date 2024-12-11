using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Models
{
    public class Drug : INotifyPropertyChanged
    {
        public int drugId { get; set; }
        public string name { get; set; }
        public string unit { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public DateOnly manufacturing_date { get; set; }

        public DateOnly expiry_date { get; set; }
        public string drugTypeName { get; set; }

        public Drug(Drug drug)
        {
            this.drugId = drug.drugId;
            this.name = drug.name;
            this.unit = drug.unit;
            this.price = drug.price;
            this.quantity = 0;
            this.manufacturing_date = drug.manufacturing_date;
            this.expiry_date = drug.expiry_date;
            this.drugTypeName = drug.drugTypeName;
        }

        public Drug()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}