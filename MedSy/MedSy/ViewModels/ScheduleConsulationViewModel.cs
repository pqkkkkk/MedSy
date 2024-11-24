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
using MedSy.Services.Patient;
using MedSy.Services.User;
using Microsoft.UI.Xaml;

namespace MedSy.ViewModels
{
    public class ScheduleConsulationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Models.Patient> patients { get; set; }

        public Models.Patient selectedPatient { get; set; }
        public Models.Doctor selectedDoctor { get; set; }

        public List<string> Form { get; set; } = new List<string> { "Online", "Offline" };

        public string selectedForm { get; set; }
        public Models.User currentUser = (Application.Current as App).locator.currentUser;
        public System.DateTimeOffset? _consultationDate { get; set; }
        public System.TimeSpan selected_startTime { get; set; }
        public System.TimeSpan selected_endTime { get; set; }
        public string _reason { get; set; }

        public string _status = "New";

        void LoadData()
        {
            // Khởi tạo các đối số cần thiết và tìm bệnh nhân có id giống với id của current User

            PatientMockDao patientDao = new PatientMockDao();
            patients = new ObservableCollection<Patient>(patientDao.GetAllPatient());
            if (currentUser != null && currentUser.role == "patient")
            {
                // set bệnh nhân được chọn có id trùng với id của currentUser
                selectedPatient = patients.FirstOrDefault(p => p.id == currentUser.id);
            }
        }
        public ScheduleConsulationViewModel(Models.Doctor d)
        {
            selectedDoctor = new Models.Doctor();
            selectedDoctor = d;
            selected_startTime = new System.TimeSpan(12,0,0);
            selected_endTime = new System.TimeSpan(12, 0, 0);
            LoadData();
        }

        public void UpdateConsulation()
        {
            IConsultationDao dao = (Application.Current as App).locator.consultationDao;
           
        }

        public bool CreateConsultation()
        {
            IConsultationDao dao = (Application.Current as App).locator.consultationDao;
            return dao.createConsultation(DateOnly.FromDateTime(_consultationDate.Value.DateTime), TimeOnly.FromTimeSpan(selected_startTime), TimeOnly.FromTimeSpan(selected_endTime), selectedForm, _status, selectedPatient.id, selectedDoctor.id, "", _reason);
        }
    }
}
