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
        public ObservableCollection<User> connectingUsers { get; set; }
        public User selectedUser { get; set; }
        public ChatViewModel()
        {
            var messageList = (Application.Current as App).locator.messageDao.getMessages(0,0);
            messages = new ObservableCollection<Message>(messageList);

            int currentUserId = (Application.Current as App).locator.currentUser.id;
            string currentRole = (Application.Current as App).locator.currentUser.role;
            var connectingUserList = (Application.Current as App).locator.managementDao.getConnectingUsers(currentUserId, currentRole);
            connectingUsers = new ObservableCollection<User>(connectingUserList);

            socketService = (Application.Current as App).locator.socketService;
            socketService.MessageReceived += onMessageReceived;

            selectedUser = new User();
            selectedUser = null;

            
        }
        public void onMessageReceived(object sender, Tuple<string,int> data)
        {            
            var (message, senderId) = data;

            if(selectedUser == null || senderId != selectedUser.id)
            {
                for (int i = 0; i < connectingUsers.Count; i++)
                {
                    if (connectingUsers[i].id == senderId)
                    {
                        (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
                        {
                            connectingUsers[i].newMessage = true;
                            int currentUserId = (Application.Current as App).locator.currentUser.id;
                            string currentRole = (Application.Current as App).locator.currentUser.role;
                            (Application.Current as App).locator.messageDao.addMessage(senderId, currentUserId, message); // remove when using sqlDao
                            (Application.Current as App).locator.managementDao.onNewMessageNotify(currentUserId, senderId, currentRole);
                        });                        
                        break;
                    }
                }
            }
            else
            {
                (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
                {
                    messages.Add(new Message { content = message, senderId = senderId, receiverId = 1 });
                });  
            }
        }
        public void loadMessages(int currentUserId, int oppositeUserId)
        {
            IMessageDao messageDao = (Application.Current as App).locator.messageDao;
            messages = new ObservableCollection<Message>(messageDao.getMessages(currentUserId, oppositeUserId));
        }
        public void updateSelectedUser(User selectedUser)
        {
            this.selectedUser = selectedUser;
            this.selectedUser.newMessage = false;

            int currentUserId = (Application.Current as App).locator.currentUser.id;
            int oppositeUserId = selectedUser.id;
            string currentRole = (Application.Current as App).locator.currentUser.role;
            (Application.Current as App).locator.managementDao.offNewMessageNotify(currentUserId, oppositeUserId, currentRole);
        }
        public async Task sendMessage(string message, int senderId, int receiverId)
        {
            await socketService.sendMessage(message, senderId, receiverId);
            (Application.Current as App).locator.messageDao.addMessage(senderId, receiverId, message);
            messages.Add(new Message { content = message, senderId = senderId, receiverId = receiverId });
            (Application.Current as App).locator.managementDao.onMySelfNewMessageNotify(senderId, receiverId, (Application.Current as App).locator.currentUser.role);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
