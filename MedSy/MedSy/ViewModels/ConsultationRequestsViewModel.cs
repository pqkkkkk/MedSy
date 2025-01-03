using MedSy.Models;
using MedSy.Services.Consultation;
using MedSy.Services.Drug;
using MedSy.Services.Prescription;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.ViewModels
{
    public class ConsultationRequestsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.Consultation> consultations { get; set; }
        public Models.Consultation selectedConsultation { get; set; }
        public ObservableCollection<PrescriptionDetail> prescriptionDetails { get; set; }
        public string selectedStatus { get; set; }
        public Models.Consultation nextConsultationToday { get; set; }
        public Models.User nextConsultationUser { get; set; }
        public int CRNotification { get; set; }
        public ConsultationRequestsViewModel()
        {
            selectedConsultation = null;
            selectedStatus = "All";
            CRNotification = 0;
            prescriptionDetails = new ObservableCollection<PrescriptionDetail>();
            (Application.Current as App).locator.socketService.newCRReceived += OnCRNotification;
            (Application.Current as App).locator.socketService.acceptedCRNotiReceived += OnCRNotification;
            updateAllMissedConsultations();
            getConsultations(null,null,null);
            getNextConsultationTodayInfo();
        }
        public void LoadPrescriptionDetailsOfConsultation()
        {
            IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
            prescriptionDetails = new ObservableCollection<PrescriptionDetail>(prescriptionDao.getPrescriptionDetails(selectedConsultation.id));
        }
        public void LoadDrugOfCorrespondingPrescriptionDetail()
        {
            IDrugDao drugDao = (Application.Current as App).locator.drugDao;
            for (int i = 0; i < prescriptionDetails.Count(); i++)
            {
                prescriptionDetails[i].drug = drugDao.getDrugById(prescriptionDetails[i].drug_id);
                //prescriptionDetails[i].drug = drugs.FirstOrDefault(d => d.drugId == prescriptionDetails[i].drug_id);
            }

        }
        public void getConsultations(DateOnly? date, TimeOnly? startTime, TimeOnly? endTime)
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            int userId = (Application.Current as App).locator.currentUser.id;
            string userRole = (Application.Current as App).locator.currentUser.role;
            consultations = new ObservableCollection<Models.Consultation>(consultationDao.GetConsultations(userRole,userId, selectedStatus, date,startTime,endTime));
        }
        public void getNextConsultationTodayInfo()
        {
            int userId = (Application.Current as App).locator.currentUser.id;
            string userRole = (Application.Current as App).locator.currentUser.role;
            nextConsultationToday = (Application.Current as App).locator.consultationDao.GetNextConsultationToday(userRole, userId);

            if (nextConsultationToday != null)
            {
                if (userRole == "patient")
                    nextConsultationUser = (Application.Current as App).locator.userDao.getUserById(nextConsultationToday.doctorId);
                else if (userRole == "doctor")
                    nextConsultationUser = (Application.Current as App).locator.userDao.getUserById(nextConsultationToday.patientId);
            }
            if(nextConsultationUser == null)
            {
                nextConsultationUser = new Models.User()
                {
                    fullName = "None"
                };
            }
        }
        public void updateAllMissedConsultations()
        {
            string userRole = (Application.Current as App).locator.currentUser.role;
            int userId = (Application.Current as App).locator.currentUser.id;
            (Application.Current as App).locator.consultationDao.UpdateAllMissedConsultations(userRole,userId);
        }
        public void updateSelectedConsultation(Models.Consultation consultation)
        {
            selectedConsultation = consultation;
        }
        public void updateSelectedStatus(string status)
        {
            selectedStatus = status;
        }
        public void searchAndFilterConsultations(DateOnly? date, TimeOnly? startTime, TimeOnly? endTime)
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            int userId = (Application.Current as App).locator.currentUser.id;
            string userRole = (Application.Current as App).locator.currentUser.role;
            consultations = new ObservableCollection<Models.Consultation>(consultationDao.GetConsultations(userRole,userId, selectedStatus, date,startTime, endTime));
        }
        public int acceptRequest()
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            List<Models.Consultation> sameDayConsultationList = consultationDao.GetConsultations("doctor", selectedConsultation.doctorId, "Accepted", selectedConsultation.date, null, null);
            int sameTimeConsultationCount = sameDayConsultationList.Count(c => c.startTime == selectedConsultation.startTime && c.endTime == selectedConsultation.endTime);
            if (sameTimeConsultationCount > 0)
            {
                return -1;
            }
            if (consultationDao.UpdateStatus(selectedConsultation, "Accepted") == 1){

                selectedConsultation.status = "Accepted";
                return 1;
            }
            return 0;

        }
        public int rejectRequest()
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            if(consultationDao.DeleteConsultation(selectedConsultation) == 1)
            {
                consultations.Remove(selectedConsultation);
                return 1;

            }
            return 0;
        }
        public int cancelRequest()
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            if (consultationDao.DeleteConsultation(selectedConsultation) == 1)
            {
                consultations.Remove(selectedConsultation);
                return 1;

            }
            return 0;
        }
        public int UpdateNextConsultationTodayToDone()
        {
            if(nextConsultationToday != null)
            {
                (Application.Current as App).locator.consultationDao.UpdateStatus(nextConsultationToday,"Done");
                nextConsultationUser = null;
                getNextConsultationTodayInfo();
                return 1;
            }
            return 0;
        }
        public void OnCRNotification()
        {
            (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                CRNotification = 1;
            });
        }
        public void OffCRNotification()
        {
            (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                CRNotification = 0;
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
