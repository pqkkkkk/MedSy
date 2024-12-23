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
    public string paymentUrl { get; set; }
    public PrescriptionPaymentViewModel()
    {
        currentUser = (Application.Current as App).locator.currentUser;
        prescriptions = new List<Prescription>();
        selectedPrescription = null;
        paymentUrl = "";
        LoadData("unpaid");
        LoadPrescriptionDetails();
    }
    public void InitializeSelectedPrescription()
    {
        selectedPrescription = new Prescription()
        {
            totalPrice = 0,
            created_day = DateOnly.FromDateTime(DateTime.Now),
            consultationId = -1,
            status = "",
        };
    }
    public void LoadData(string prescriptionStatus)
    {
        IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
        prescriptions = prescriptionDao.GetPrescriptions(currentUser.id,prescriptionStatus);
    }

    public void LoadPrescriptionDetails()
    {
        IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
        if(selectedPrescription != null)
            prescriptionDetails = new ObservableCollection<PrescriptionDetail>(prescriptionDao.getPrescriptionDetails_ByPrescriptionId(selectedPrescription.prescriptionId));
        else
            prescriptionDetails = new ObservableCollection<PrescriptionDetail>();

        IDrugDao drugDao = (Application.Current as App).locator.drugDao;
        for (int i = 0; i < prescriptionDetails.Count(); i++)
        {
            prescriptionDetails[i].drug = drugDao.getDrugById(prescriptionDetails[i].drug_id);
            //prescriptionDetails[i].drug = drugs.FirstOrDefault(d => d.drugId == prescriptionDetails[i].drug_id);
        }
    }
    public void UpdatePrescriptionStatus()
    {
        IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
        prescriptionDao.UpdatePrescriptionStatus(selectedPrescription.prescriptionId);
    }
}


