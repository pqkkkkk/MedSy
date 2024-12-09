using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using MedSy.Services.Consultation;
using MedSy.Services.Drug;
using MedSy.Services.Prescription;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MedSy.ViewModels
{
    class PresciptionPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Models.Consultation selectedConsultation { get; set; }
        public Models.User selectedUser { get; set; }
        public ObservableCollection<Drug> drugs { get; set; }
        public ObservableCollection<Drug> availableDrugs { get; set; }

        public ObservableCollection<Drug> selecteddrugs { get; set; }
        public ObservableCollection<string> types { get; set; }

        public string Keyword { get; set; } = "";

        public string selectedType { get; set; }
        public Drug selectedDrug { get; set; }
        public Drug selecteddrugsItem { get; set; }
        public Dictionary<Drug, string> DrugIndications { get; } = new();

        IDrugDao drugDao { get; set; }
        public PresciptionPageViewModel()
        {
            drugDao = (Application.Current as App).locator.drugDao;
            types = new ObservableCollection<string>(drugDao.getAllDrugType());
            types.Add("All");
            selectedType = types.FirstOrDefault(name => name == "All");
            drugs = new ObservableCollection<Drug>(drugDao.getAllDrugs(Keyword, selectedType));
            availableDrugs = drugs;
            selecteddrugs ??= new ObservableCollection<Drug>();
            selectedConsultation = new Consultation();
            selectedUser = new User();
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
            if (selecteddrugsItem == null)
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

        public int createPrescriptionAndUpdateQuantity()
        {
            if(selecteddrugs.Count() == 0)
            {
                return -1;
            }
            IPrescriptionDao dao = (Application.Current as App).locator.prescriptionDao;
            // Tạo prescripption mới
            if (dao.createPrescription(0, DateOnly.FromDateTime(DateTime.Now), selectedConsultation.id))
            {
                // nếu tạp thành công, lấy prescriptionId
                int prescriptionId = dao.getPrescriptionId_ByConsultationId(selectedConsultation.id);
                int total = 0;

                // Với mỗi item trong danh sách thuốc đã được chọn, thêm một prescription_detail
                foreach (var item in selecteddrugs)
                {
                    string indication = DrugIndications.TryGetValue(item, out var value) ? value : "No Indication";
                    dao.insertIntoPrescriptionDetail(item.quantity, indication, prescriptionId, item.drugId);
                    total += item.price;

                    // cập nhật lại số lượng thuốc trong kho
                    var currentDrug = drugs.FirstOrDefault(d => d.drugId == item.drugId);
                    drugDao.updateQuantity(currentDrug.quantity, currentDrug.drugId);
                }
                if (dao.updateTotalPrice(total, prescriptionId) != 1) {
                    return 0;
                };
                IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
                consultationDao.updateResult(selectedConsultation.id,selectedConsultation.result);
                return 1;
            }
            return 0;
        
        }



        public void SaveIndicationForDrug(Drug drug, string indication)
        {
            if (DrugIndications.ContainsKey(drug))
                DrugIndications[drug] = indication;
            else
                DrugIndications.Add(drug, indication);
        }

        public string GetIndicationForDrug(Drug drug)
        {
            return DrugIndications.ContainsKey(drug) ? DrugIndications[drug] : string.Empty;
        }
    }
}