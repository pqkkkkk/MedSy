using MedSy.Models;
using MedSy.Services;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using Org.BouncyCastle.Utilities.Collections;
using System.Diagnostics;
namespace MedSy.ViewModels
{
    public class ChatBotViewModel : INotifyPropertyChanged
    {
        private static int currentUserId = (Application.Current as App).locator.currentUser.id;
        public ObservableCollection<Message> messageList { get; set; }
        public int HasNewCR { get; set; }
        public int HasConsultationToday { get; set; }
        public ChatBotViewModel()
        {
            messageList = new ObservableCollection<Message>();
        }
        public void addNewCRNotification()
        {
            messageList.Add(new Message
            {
                content = $"You have {HasNewCR} new consultation requests",
                senderId = -1,
                receiverId = currentUserId
            });
        }
        public void addConsultationTodayNotification()
        {
            messageList.Add(new Message
            {
                content = $"You have {HasConsultationToday} consultation appoiments today",
                senderId = -1,
                receiverId = currentUserId
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
