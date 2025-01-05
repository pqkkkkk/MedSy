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
using MedSy.Services.Consultation;
namespace MedSy.ViewModels
{
    public class ChatBotViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Message> messageList { get; set; }
        public int HasNewCR { get; set; }
        public int HasConsultationToday { get; set; }
        public Consultation nextConsultationToday { get; set; }
        public ObservableCollection<Consultation> consultationTodayList { get; set; }
        public ObservableCollection<Message> questionOptionList { get; set; }
        public ObservableCollection<Consultation> newCRList { get; set; }
        public User currentUser { get; set; }
        public delegate void ChatBotUpdateEventHandler();
        public event ChatBotUpdateEventHandler ChatBotUpdate;
        public ChatBotViewModel()
        {
            messageList = new ObservableCollection<Message>();
            currentUser = (Application.Current as App).locator.currentUser;
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            nextConsultationToday = consultationDao.GetNextConsultationToday(currentUser.role, currentUser.id);
            consultationTodayList = new ObservableCollection<Consultation>(consultationDao.GetConsultations(currentUser.role,currentUser.id,"Accepted",DateOnly.FromDateTime(DateTime.Now),null,null));
            newCRList = new ObservableCollection<Consultation>(consultationDao.GetConsultations(currentUser.role, currentUser.id, "New", null, null, null));
            LoadQuestionOptionList();
            (Application.Current as App).locator.timerService.OnTimerElapsed += LoadData;
        }

        public void LoadQuestionOptionList()
        {
            questionOptionList = new ObservableCollection<Message>()
            {
                new Message() { content = "None", senderId = -1, receiverId = currentUser.id },
                new Message() { content = "Do I have any consultation appointment today?", senderId = -1, receiverId = currentUser.id },
                new Message() { content = "List detail of each consultation appointment today for me", senderId = -1, receiverId = currentUser.id },
            };
            if(currentUser.role == "doctor")
            {
                questionOptionList.Add(new Message() { content = "Do I have any new consultation request?", senderId = -1, receiverId = currentUser.id });
            }
        }
        public void addNewCRNotification()
        {
            messageList.Add(new Message
            {
                content = $"You have {HasNewCR} new consultation requests",
                senderId = -1,
                receiverId = currentUser.id
            });
        }
        public void addConsultationTodayNotification()
        {
            messageList.Add(new Message
            {
                content = $"You have {HasConsultationToday} consultation appoiments today",
                senderId = -1,
                receiverId = currentUser.id
            });
        }
        public void addNextConsultationTodayDetailNotification()
        {
            string result = "";
            result += "Next consultation: "; 
            result += $"Start time: {nextConsultationToday.startTime} - ";
            result += $"End time: {nextConsultationToday.endTime} - ";
            result += $"Sypmtom of patient: {nextConsultationToday.reason} - ";
            result += "-----------------------------------\n";
            
            messageList.Add(new Message
            {
                content = result,
                senderId = -1,
                receiverId = currentUser.id
            });
        }
        public void LoadData()
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                consultationTodayList = new ObservableCollection<Consultation>(consultationDao.GetConsultations(currentUser.role, currentUser.id, "Accepted", DateOnly.FromDateTime(DateTime.Now), null, null));
                nextConsultationToday = consultationDao.GetNextConsultationToday(currentUser.role, currentUser.id);
                newCRList = new ObservableCollection<Consultation>(consultationDao.GetConsultations(currentUser.role, currentUser.id, "New", null, null, null));
                messageList.Clear();
                HasNewCR = newCRList.Count;
                HasConsultationToday = consultationTodayList.Count;
                if (HasNewCR > 0)
                    addNewCRNotification();
                if (HasConsultationToday > 0)
                    addConsultationTodayNotification();
                if (nextConsultationToday != null)
                    addNextConsultationTodayDetailNotification();

                ChatBotUpdate?.Invoke();
            });
           
        }
        public void GetAnswer(string question)
        {
            IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
            consultationTodayList = new ObservableCollection<Consultation>(consultationDao.GetConsultations(currentUser.role, currentUser.id, "Accepted", DateOnly.FromDateTime(DateTime.Now), null, null));
            nextConsultationToday = consultationDao.GetNextConsultationToday(currentUser.role, currentUser.id);
            newCRList = new ObservableCollection<Consultation>(consultationDao.GetConsultations(currentUser.role, currentUser.id, "New", null, null, null));
            HasNewCR = newCRList.Count;
            HasConsultationToday = consultationTodayList.Count;

            messageList.Add(new Message
            {
                content = question,
                senderId = currentUser.id,
                receiverId = -1
            });
            string answer = "I don't understand your question";
            switch (question)
            {
                case "Do I have any new consultation request?":
                    answer =  $"You have {HasNewCR} new consultation requests";
                    break;
                case "Do I have any consultation appointment today?":
                    answer =  $"You have {HasConsultationToday} consultation appoiments today";
                    break;
                case "List detail of each consultation appointment today for me":
                    string result = "";
                    foreach (var consultation in consultationTodayList)
                    {
                        result += $"Start time: {consultation.startTime} - ";
                        result += $"End time: {consultation.endTime} - ";
                        result += $"Sypmtom of patient: {consultation.reason} - ";
                        result += "-----------------------------------\n";
                    }
                    if(result == "")
                        result = "You don't have any consultation appointment today";
                    answer = result;
                    break;
                default:
                    break;
            }

            messageList.Add(new Message
            {
                content = answer,
                senderId = -1,
                receiverId = currentUser.id
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
