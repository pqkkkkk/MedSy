using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using MedSy.Services.Drug;
using MedSy.Services.Prescription;
using Microsoft.UI.Xaml;

namespace MedSy.ViewModels;
public class PrescriptionPaymentViewModel:INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public ObservableCollection<PrescriptionDetail> prescriptionDetails { get; set; }
    public Prescription selectedPrescription { get; set; }
    public List<Prescription> prescriptions { get; set; }
    public User currentUser { get; set; }
    public PrescriptionPaymentViewModel()
    {
        currentUser = (Application.Current as App).locator.currentUser;
        prescriptions = new List<Prescription>();
        selectedPrescription = new Prescription();
        LoadData();
        LoadPrescriptionDetails();
    }

    void LoadData()
    {
        IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
        prescriptions = prescriptionDao.GetPrescriptions(currentUser.id);
    }

    public void LoadPrescriptionDetails()
    {
        IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
        prescriptionDetails = new ObservableCollection<PrescriptionDetail>(prescriptionDao.getPrescriptionDetails_ByPrescriptionId(selectedPrescription.prescriptionId));
        
        IDrugDao drugDao = (Application.Current as App).locator.drugDao;
        for (int i = 0; i < prescriptionDetails.Count(); i++)
        {
            prescriptionDetails[i].drug = drugDao.getDrugById(prescriptionDetails[i].drug_id);
            //prescriptionDetails[i].drug = drugs.FirstOrDefault(d => d.drugId == prescriptionDetails[i].drug_id);
        }

    }
}


