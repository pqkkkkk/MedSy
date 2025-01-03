using MedSy.Models;
using MedSy.Services.Consultation;
using MedSy.Services.Management;
using MedSy.Services.User;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.ViewModels
{
    public class PatientManagementViewModel : INotifyPropertyChanged
    {
        
        public ObservableCollection<Models.PatientManagementItem> patients { get; set; }
        public Models.PatientManagementItem selectedPatientItem { get; set; }
        public Models.Consultation selectedConsultation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public PatientManagementViewModel()
        {
            IManagementDao managementDao = (Application.Current as App).locator.managementDao;
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            IUserDao userDao = (Application.Current as App).locator.userDao;
            int userId = (Application.Current as App).locator.currentUser.id;
            string userRole = (Application.Current as App).locator.currentUser.role;

            var list = managementDao.getConnectingUsers(userId, userRole);
            patients = new ObservableCollection<Models.PatientManagementItem>();
            foreach (var item in list)
            {
                ObservableCollection<Models.Consultation> doneConsultations = new ObservableCollection<Models.Consultation>(consultationDao.GetAllConsultationsByDoctorIdAndPatientId(userId,item.id,"Done",null,null,null));
                ObservableCollection<Models.Consultation> todayConsultations = new ObservableCollection<Models.Consultation>(consultationDao.GetAllConsultationsByDoctorIdAndPatientId(userId, item.id, "Accepted", DateOnly.FromDateTime(DateTime.Now), null, null));
                var patient = userDao.getUserById(item.id);
                patients.Add(new Models.PatientManagementItem { patient = patient, doneConsultations = doneConsultations, todayConsultations = todayConsultations });
            }

            selectedConsultation = new Models.Consultation();
            selectedPatientItem = patients.Count == 0 ? new Models.PatientManagementItem() : patients[0];
        }
        public void UpdateSelectedConsultation(int consultationId)
        {
            selectedConsultation = patients.SelectMany(x => x.doneConsultations).FirstOrDefault(x => x.id == consultationId);
        }
        public void UpdateSelectedPatientItem(PatientManagementItem item)
        {
            selectedPatientItem = item;
        }
    }
}
