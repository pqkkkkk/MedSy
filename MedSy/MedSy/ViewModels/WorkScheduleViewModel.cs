using MedSy.Services.Consultation;
using Microsoft.UI.Xaml;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MedSy.ViewModels
{
    public class WorkScheduleViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.Consultation> consultations { get; set; }
        public ObservableCollection<ObservableCollection<Models.WorkScheduleMarkerItem>> marker { get; set; }
        public WorkScheduleViewModel()
        {
            getConsultations(DateOnly.FromDateTime(DateTime.Now));

            marker = new ObservableCollection<ObservableCollection<Models.WorkScheduleMarkerItem>>();
            createMarker();
            updateMarker();
        }
        public void getConsultations(DateOnly date)
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            int doctorId = (Application.Current as App).locator.currentUser.id;
            consultations = new ObservableCollection<Models.Consultation>(consultationDao.GetConsultationsInAWeek(doctorId,date));
        }
        public void createMarker()
        {
            for (int i = 0; i < 25; i++)
            {
                var temp = new ObservableCollection<Models.WorkScheduleMarkerItem>();
                for (int j = 0; j < 8; j++)
                {
                    var item = new Models.WorkScheduleMarkerItem()
                    {
                        isMarked = false
                    };
                    temp.Add(item);
                }

                marker.Add(temp);

                
            }
            
        }
            
        public void updateMarker()
        {
            for(int i =0;i<consultations.Count;i++)
            {
                int dayOfWeek = (int)consultations[i].date.DayOfWeek;
                int hour = consultations[i].startTime.Hour;
                if (hour != 23)
                    marker[hour + 1][dayOfWeek + 1].isMarked = true;
                else
                    marker[1][dayOfWeek + 1].isMarked = true;
            }
            NotifyPropertyChanged(nameof(marker));
        }
        
        public void refreshMarker()
        {
            for (int i = 0; i < 25; i++)
            { 
                for (int j = 0; j < 8; j++)
                {
                    marker[i][j].isMarked = false;
                }
            }
            
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
