using MedSy.Services.Consultation;
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
        public string selectedStatus { get; set; }
        public Models.Consultation nextConsultationToday { get; set; }
        public Models.User nextConsultationUser { get; set; }
        public int CRNotification { get; set; }
        public ConsultationRequestsViewModel()
        {
            selectedConsultation = null;
            selectedStatus = "All";
            CRNotification = 0;
            (Application.Current as App).locator.socketService.newCRReceived += OnCRNotification;
            (Application.Current as App).locator.socketService.acceptedCRNotiReceived += OnCRNotification;
            getConsultations(null,null,null);
            getNextConsultationTodayInfo();
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
   
           if(consultationDao.UpdateStatus(selectedConsultation, "Accepted") == 1){

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
