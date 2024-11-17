using MedSy.Models;
using MedSy.Services;
using MedSy.Services.Management;
using MedSy.Services.Message;
using MedSy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Helpers
{
    public class Locator
    {
        public User currentUser { get; set; }

        public List<User> users { get; set; } = new List<User>();
        public IMessageDao messageDao { get; set; }
        public IManagementDao managementDao { get; set; }
        public SocketService socketService { get; set; }
        public MainWindow mainWindow { get; set; }
        public ChatViewModel chatViewModel { get; set; }
        
    }

}
