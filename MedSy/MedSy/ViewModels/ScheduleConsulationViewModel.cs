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

        public List<string> Form { get; set; } = new List<string> { "Online", "Offline" };
        public List<string> pathology { get; set; }
        public string selectedPathology { get; set; }
        public string selectedForm { get; set; }
        public Models.User currentUser = (Application.Current as App).locator.currentUser;
        public System.DateTimeOffset? _consultationDate { get; set; }
        public System.TimeSpan selected_startTime { get; set; }
        public System.TimeSpan selected_endTime { get; set; }
        public string _reason { get; set; }

        public string _status = "New";

        //void LoadData()
        //{
            

        //    PatientMockDao patientDao = new PatientMockDao();
        //    patients = new ObservableCollection<Patient>(patientDao.GetAllPatient());
        //    if (currentUser != null && currentUser.role == "patient")
        //    {
        //        selectedPatient = new Patient()
        //        {
        //            id = currentUser.id,
        //            username = currentUser.username,
        //            email = currentUser.email,
        //            fullName = currentUser.fullName,
        //            password = currentUser.password,
        //            phoneNumber = currentUser.phoneNumber,
        //            gender = currentUser.gender,
        //            address = currentUser.address,
        //            healthInsurance = true
        //        };
        //    }
        //}
        public ScheduleConsulationViewModel(Models.Doctor d)
        {
            selectedDoctor = new Models.Doctor();
            selectedDoctor = d;
            selected_startTime = new System.TimeSpan(12,0,0);
            selected_endTime = new System.TimeSpan(12, 0, 0);
            pathology = new List<string>();
            selectedPathology = "";
            IConsultationDao dao = (Application.Current as App).locator.consultationDao;
            pathology = dao.getAllPathology();
            selectedPatient = (Application.Current as App).locator.currentUser;
            //LoadData();
        }

        public void UpdateConsulation()
        {
            IConsultationDao dao = (Application.Current as App).locator.consultationDao;
           
        }

        public bool CreateConsultation()
        {
            IConsultationDao dao = (Application.Current as App).locator.consultationDao;

            (Application.Current as App).locator.socketService.sendNewCRMessage(selectedDoctor.id, currentUser.id);
            return dao.createConsultation(DateOnly.FromDateTime(_consultationDate.Value.DateTime), TimeOnly.FromTimeSpan(selected_startTime), TimeOnly.FromTimeSpan(selected_endTime), selectedForm, _status, selectedPatient.id, selectedDoctor.id, "", _reason,selectedPathology);
        }
    }
}
