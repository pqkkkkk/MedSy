using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using MedSy.Services.Drug;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MedSy.ViewModels
{
    class PresciptionPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Drug> drugs { get; set; }
        public ObservableCollection<Drug> availableDrugs { get; set; }

        public ObservableCollection<Drug> selecteddrugs { get; set; }
        public ObservableCollection<string> types { get; set; }

        public string Keyword { get; set; } = "";

        public string selectedType { get; set; }
        public Drug selectedDrug { get; set; }
        public Drug selecteddrugsItem { get; set; }
        IDrugDao drugDao {  get; set; }
        public PresciptionPageViewModel()
        {
            drugDao = (Application.Current as App).locator.drugDao;
            types = new ObservableCollection<string>(drugDao.getAllDrugType());
            types.Add( "All");
            selectedType = types.FirstOrDefault(name =>name == "All");
            drugs = new ObservableCollection<Drug>(drugDao.getAllDrugs(Keyword, selectedType));
            availableDrugs = drugs;
            selecteddrugs ??= new ObservableCollection<Drug>();

        }

        public void LoadData()
        {
            if (selectedType == "All")
            {
                availableDrugs = new ObservableCollection<Drug>(drugs.Where(d => d.name.Contains(Keyword, StringComparison.OrdinalIgnoreCase)));

            }
            else
            {
                availableDrugs = new ObservableCollection<Drug>(drugs.Where(d => d.drugTypeName == selectedType && d.name.Contains(Keyword, StringComparison.OrdinalIgnoreCase)).ToList());
            }
        }

        public void AddIntoSelectedDrugs()
        {
            var item = selecteddrugs.FirstOrDefault(i => i.name == selectedDrug.name);
            if (item == null)
            {
                item = new Drug(selectedDrug);
                selecteddrugs.Add(item);
            }
            item.quantity++;
        }

        public void search()
        {
            LoadData();
        }

        public bool minus_click()
        {   
            if(selecteddrugsItem == null)
            {
                return false;
            }
            var drugInDrugs = drugs.FirstOrDefault(d => d.name == selecteddrugsItem.name);

            if (selecteddrugsItem.quantity == 1)
            {
                selecteddrugs.Remove(selecteddrugsItem);
            }
            else
            {
                selecteddrugsItem.quantity--;
                selecteddrugsItem.price -= drugInDrugs.price;
            };
            drugInDrugs.quantity++;
            return true;
        }

        public bool plus_click()
        {
            if (selecteddrugsItem == null)
            {
                return false;
            }
            var drugInDrugs = drugs.FirstOrDefault(d => d.name == selecteddrugsItem.name);
            if (selectedDrug.quantity == 0)
            {
                return false;
            }
            else
            {
                selecteddrugsItem.quantity++;
                selecteddrugsItem.price += drugInDrugs.price;
            };
            drugInDrugs.quantity--;
            return true;
        }

        public void update()
        {
            drugDao.updateQuantity(selectedDrug.quantity, selectedDrug.drugId);
        }
    }
}
