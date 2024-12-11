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
        public ObservableCollection<PrescriptionDetail> prescriptionDetails { get; set; }
        public PrescriptionDetail selectedPrescriptionDetail { get; set; }
        public bool prescribed { get; set; }
        public ObservableCollection<string> types { get; set; }
        public string Keyword { get; set; } = "";
        public string selectedType { get; set; }
        public Drug selectedDrug { get; set; }
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
            selectedConsultation = new Consultation();
            selectedUser = new User();
            prescriptionDetails = null;
        }
        public void LoadPrescriptionDetails()
        {
            IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
            prescriptionDetails = new ObservableCollection<PrescriptionDetail>(prescriptionDao.getPrescriptionDetails(selectedConsultation.id));
            if(prescriptionDetails.Count() == 0)
            {
                prescribed = false;
            }
            else
            {
                prescribed = true;
            }
        }
        public void LoadDrugOfCorrespondingPrescriptionDetail()
        {
            IDrugDao drugDao = (Application.Current as App).locator.drugDao;
            for (int i =0;i< prescriptionDetails.Count(); i++)
            {
                prescriptionDetails[i].drug = drugDao.getDrugById(prescriptionDetails[i].drug_id);
                //prescriptionDetails[i].drug = drugs.FirstOrDefault(d => d.drugId == prescriptionDetails[i].drug_id);
            }
            
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
            var item = prescriptionDetails.FirstOrDefault(i => i.drug.name == selectedDrug.name);
            if (item == null)
            {
                item = new PrescriptionDetail()
                {
                    id = -1,
                    quantity = 0,
                    drug = selectedDrug,
                    price = selectedDrug.price,
                    usage = ""
                };
                prescriptionDetails.Add(item);
            }
            item.quantity++;
        }

        public void search()
        {
            LoadData();
        }

        public bool minus_click()
        {
            if (selectedPrescriptionDetail == null)
            {
                return false;
            }
            var drugInDrugs = drugs.FirstOrDefault(d => d.name == selectedPrescriptionDetail.drug.name);
            selectedDrug = drugInDrugs;
            if (selectedPrescriptionDetail.quantity == 1)
            {
                prescriptionDetails.Remove(selectedPrescriptionDetail);
            }
            else
            {
                selectedPrescriptionDetail.quantity--;
                selectedPrescriptionDetail.price -= drugInDrugs.price;
            };
            drugInDrugs.quantity++;
            return true;
        }

        public bool plus_click()
        {
            if (selectedPrescriptionDetail == null)
            {
                return false;
            }
            var drugInDrugs = drugs.FirstOrDefault(d => d.name == selectedPrescriptionDetail.drug.name);
            selectedDrug = drugInDrugs;
            if (selectedDrug.quantity == 0)
            {
                return false;
            }
            else
            {
                selectedPrescriptionDetail.quantity++;
                selectedPrescriptionDetail.price += drugInDrugs.price;
            };
            drugInDrugs.quantity--;
            return true;
        }

        public int createPrescriptionAndUpdateQuantity()
        {
            if(prescriptionDetails.Count() == 0)
            {
                return -1;
            }
           
            IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
            
            if (prescriptionDao.createPrescription(0, DateOnly.FromDateTime(DateTime.Now), selectedConsultation.id))
            { 
                int prescriptionId = prescriptionDao.getPrescriptionId_ByConsultationId(selectedConsultation.id);
                int total = 0;

                foreach (var item in prescriptionDetails)
                {
                   
                    prescriptionDao.insertIntoPrescriptionDetail(item, prescriptionId);
                    total += item.price;

                    var currentDrug = drugs.FirstOrDefault(d => d.drugId == item.drug.drugId);
                    drugDao.updateQuantity(currentDrug.quantity, currentDrug.drugId);
                }
                if (prescriptionDao.updateTotalPrice(total, prescriptionId) != 1) {
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