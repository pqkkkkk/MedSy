using MedSy.Services;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Proximity;

namespace MedSy.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private SocketService socketService;
        public bool IsNewMessage { get; set; }
        public int HasNewCR { get; set; }
        public int HasConsultationToday { get; set; }
        public string selectedPage { get; set; }
        public bool IsNewChatBotMessage { get; set; }

        public MainPageViewModel()
        {
            socketService = (Application.Current as App).locator.socketService;
            socketService.MessageReceived += onNewMessageNotification;

            int currentUserId = (Application.Current as App).locator.currentUser.id;
            string currentRole = (Application.Current as App).locator.currentUser.role;
            IsNewMessage = (Application.Current as App).locator.managementDao.checkNewMessage(currentUserId, currentRole) == 0 ? false : true;
            HasNewCR = CheckNewCR();
            IsNewChatBotMessage = true;
            HasConsultationToday = CheckConsultationToday();
            selectedPage = "Dashboard";

            
        }
        public int CheckNewCR()
        {
            string userRole = (Application.Current as App).locator.currentUser.role;
            int userId = (Application.Current as App).locator.currentUser.id;
            int newCRCount = (Application.Current as App).locator.consultationDao.GetConsultations(userRole, userId, "New", null, null, null).Count;

            return newCRCount;
        }
        public int CheckConsultationToday()
        {
            string userRole = (Application.Current as App).locator.currentUser.role;
            int userId = (Application.Current as App).locator.currentUser.id;
            int consultationTodayCount = (Application.Current as App).locator.consultationDao.GetConsultations(userRole, userId, "Accepted", DateOnly.FromDateTime(DateTime.Now), null, null).Count;

            return consultationTodayCount;
        }
        private void onNewMessageNotification(object sender, Tuple<string,int> data)
        {
            if(selectedPage != "Chat")
            {
                (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
                {
                    IsNewMessage = true;
                });
            }
        }
        public void offNewMessageNotification()
        {
            (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                IsNewMessage = false;
            });
        }
        public void updateSelectedPage(string pageName)
        {
            selectedPage = pageName;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
