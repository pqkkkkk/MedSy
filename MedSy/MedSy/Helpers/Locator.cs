using MedSy.Models;
using MedSy.Services;
using MedSy.Services.Consultation;
using MedSy.Services.Management;
using MedSy.Services.Message;
using MedSy.Services.User;
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
        public IMessageDao messageDao { get; set; }
        public IManagementDao managementDao { get; set; }
        public IUserDao userDao { get; set; }
        public IConsultationDao consultationDao { get; set; }
        public SocketService socketService { get; set; }
        public MainWindow mainWindow { get; set; }
        
        
    }
   
}
