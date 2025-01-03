using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABI.System;
using MedSy.Models;
using MedSy.Services.Consultation;
using MedSy.Services.User;
using Microsoft.UI.Xaml;

namespace MedSy.ViewModels
{
    public class ScheduleConsulationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Models.Patient> patients { get; set; }

        public Models.User selectedPatient { get; set; }
        public Models.Doctor selectedDoctor { get; set; }
        public List<string> Form { get; set; } = new List<string> { "online", "offline" };
        public List<string> pathology { get; set; }
        public string selectedPathology { get; set; }
        public string selectedForm { get; set; }
        public Models.User currentUser { get; set; }
        public System.DateTimeOffset? consultationDate { get; set; }
        public System.TimeSpan selectedStartTime { get; set; }
        public System.TimeSpan selectedEndTime { get; set; }
        public string _reason { get; set; }

        public string _status = "New";

       
        public ScheduleConsulationViewModel(Models.Doctor d)
        {
            currentUser = (Application.Current as App).locator.currentUser;
            selectedDoctor = new Models.Doctor();
            selectedDoctor = d;
            selectedStartTime = new System.TimeSpan(12,0,0);
            selectedEndTime = new System.TimeSpan(12, 0, 0);
            pathology = new List<string>();
            selectedPathology = "";
            IConsultationDao dao = (Application.Current as App).locator.consultationDao;
            pathology = dao.getAllPathology();
            selectedPatient = (Application.Current as App).locator.currentUser;
        }

        public void UpdateConsulation()
        {
            IConsultationDao dao = (Application.Current as App).locator.consultationDao;
           
        }

        public int CreateConsultation()
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            List<Models.Consultation> sameDayConsultationList = consultationDao.GetConsultations(currentUser.role, currentUser.id, "Accepted", DateOnly.FromDateTime(consultationDate.Value.DateTime), null, null);
            int sameTimeConsultationCount = sameDayConsultationList.Count(c => c.startTime == TimeOnly.FromTimeSpan(selectedStartTime) && c.endTime == TimeOnly.FromTimeSpan(selectedEndTime));
            if (sameTimeConsultationCount > 0)
            {
                return -1;
            }
            (Application.Current as App).locator.socketService.sendNewCRMessage(selectedDoctor.id, currentUser.id);
            if (consultationDao.createConsultation(DateOnly.FromDateTime(consultationDate.Value.DateTime), TimeOnly.FromTimeSpan(selectedStartTime), TimeOnly.FromTimeSpan(selectedEndTime), selectedForm, _status, selectedPatient.id, selectedDoctor.id, "", _reason, selectedPathology))
                return 1;
            else
                return 0;
        }
    }
}
