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
        public Models.Consultation selectedConsultation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public PatientManagementViewModel()
        {
            IManagementDao managementDao = (Application.Current as App).locator.managementDao;
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            int userId = (Application.Current as App).locator.currentUser.id;
            string userRole = (Application.Current as App).locator.currentUser.role;

            var list = managementDao.getConnectingUsers(userId, userRole);
            patients = new ObservableCollection<Models.PatientManagementItem>();
            foreach (var item in list)
            {
                ObservableCollection<Models.Consultation> consultations = new ObservableCollection<Models.Consultation>(consultationDao.GetConsultations(item.role, item.id, "Done", null, null, null));
                patients.Add(new Models.PatientManagementItem { patient = item, consultations = consultations, isExpanded = false });
            }

            selectedConsultation = new Models.Consultation();
        }
        public void UpdateSelectedConsultation(int consultationId)
        {
            selectedConsultation = patients.SelectMany(x => x.consultations).FirstOrDefault(x => x.id == consultationId);
        }
    }
}
