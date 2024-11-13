using MedSy.Services.Consultation;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.ViewModels
{
    public class ConsultationRequestsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.Consultation> consultations { get; set; }
        public ConsultationRequestsViewModel()
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            int doctorId = (Application.Current as App).locator.currentUser.id;
            consultations = new ObservableCollection<Models.Consultation>(consultationDao.GetConsultations(doctorId));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
