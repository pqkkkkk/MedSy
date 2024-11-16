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
        public ConsultationRequestsViewModel()
        {
            selectedConsultation = null;
            selectedStatus = "All";
            getConsultations(selectedStatus, null);
        }
        public void getConsultations(string status, DateTime? date)
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            int doctorId = (Application.Current as App).locator.currentUser.id;
            consultations = new ObservableCollection<Models.Consultation>(consultationDao.GetConsultations(doctorId, status, date));
        }
        public void updateSelectedConsultation(Models.Consultation consultation)
        {
            selectedConsultation = consultation;
        }
        public void updateSelectedStatus(string status)
        {
            selectedStatus = status;
        }
        public void searchAndFilterConsultations(DateTime? date)
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            int doctorId = (Application.Current as App).locator.currentUser.id;
            consultations = new ObservableCollection<Models.Consultation>(consultationDao.GetConsultations(doctorId, selectedStatus, date));
        }
        public Models.Consultation FindConsultationById(int consultationId)
        {
            return consultations.FirstOrDefault(c => c.id == consultationId);
        }
        public int acceptRequest()
        {
            

            if(selectedConsultation != null)
            {
                selectedConsultation.status = "Accepted";
                return 1;
            }
            return 0;
        }
        public int rejectRequest()
        {
            

            if (selectedConsultation != null)
            {
                consultations.Remove(selectedConsultation);
                updateSelectedConsultation(null);
                return 1;
            }

            return 0;
        }
        public int cancelRequest()
        {
            

            if (selectedConsultation != null)
            {
                consultations.Remove(selectedConsultation);
                updateSelectedConsultation(null);
                return 1;
            }

            return 0;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
