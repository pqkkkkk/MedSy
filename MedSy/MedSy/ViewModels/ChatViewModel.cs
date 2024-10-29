using MedSy.Models;
using MedSy.Services;
using MedSy.Services.Message;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using SocketIO.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

namespace MedSy.ViewModels
{
    public class ChatViewModel :INotifyPropertyChanged
    {
        public SocketService socketService { get; set; }
        public ObservableCollection<Message> messages { get; set; }
        public ObservableCollection<Doctor> doctors { get; set; }
        public Doctor selectedDoctor { get; set; }
        public DispatcherQueue dispatcherQueue { get; set; }

        public ChatViewModel()
        {
            
            var messageList = (Application.Current as App).locator.messageDao.getMessages(0,0);
            messages = new ObservableCollection<Message>(messageList);

            int patientId = (Application.Current as App).locator.patientDao.getPatient().id;
            var doctorList = (Application.Current as App).locator.managementDao.getDoctors(patientId);
            doctors = new ObservableCollection<Doctor>(doctorList);

            socketService = (Application.Current as App).locator.socketService;
            socketService.MessageReceived += onMessageReceived;

            selectedDoctor = new Doctor();
            selectedDoctor = null;

            dispatcherQueue = (Application.Current as App).locator.mainWindow.DispatcherQueue;
        }
        
        public void onMessageReceived(object sender, Tuple<string,int> data)
        {            
            var (message, senderId) = data;

            if(selectedDoctor == null || senderId != selectedDoctor.id)
            {
                for (int i = 0; i < doctors.Count; i++)
                {
                    if (doctors[i].id == senderId)
                    {
                        dispatcherQueue.TryEnqueue(() =>
                        {
                            doctors[i].newMessage = true;
                            int patientId = (Application.Current as App).locator.patientDao.getPatient().id;
                            (Application.Current as App).locator.managementDao.onDoctorNewMessageNotify(patientId, senderId);
                        });                        
                        break;
                    }
                }
            }
            else
            {
                dispatcherQueue.TryEnqueue(() =>
                {
                    messages.Add(new Message { content = message, senderId = senderId, receiverId = 1 });
                });  
            }
        }
        public void loadMessages(int patientId, int doctorId)
        {
            IMessageDao messageDao = (Application.Current as App).locator.messageDao;
            messages = new ObservableCollection<Message>(messageDao.getMessages(patientId, doctorId));
        }
        public void updateSelectedDoctor(Doctor selectedDoctor)
        {
            this.selectedDoctor = selectedDoctor;
            this.selectedDoctor.newMessage = false;

            int patientId = (Application.Current as App).locator.patientDao.getPatient().id;
            int doctorId = selectedDoctor.id;
            (Application.Current as App).locator.managementDao.offDoctorNewMessageNotify(patientId, doctorId);
        }
        public async Task sendMessage(string message, int senderId, int receiverId)
        {
            await socketService.sendMessage(message, senderId, receiverId);
            
            messages.Add(new Message { content = message, senderId = senderId, receiverId = receiverId });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
