using MedSy.Models;
using MedSy.Services;
using MedSy.Services.Consultation;
using MedSy.Services.Drug;
using MedSy.Services.Management;
using MedSy.Services.Message;
using MedSy.Services.Prescription;
using MedSy.Services.User;
using MedSy.ViewModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Helpers
{
    public class Locator
    {
        public string databaseConnectionString { get; set; }
        public SqlConnection sqlConnection { get; set; }
        public User currentUser { get; set; }
        public List<User> users { get; set; } = new List<User>();
        public IMessageDao messageDao { get; set; }
        public IManagementDao managementDao { get; set; }
        public IUserDao userDao { get; set; }
        public IDrugDao drugDao { get; set; }
        public IConsultationDao consultationDao { get; set; }
        public IPrescriptionDao prescriptionDao { get; set; }
        public SocketService socketService { get; set; }
        public ChatBotService chatBotService { get; set; }
        public PaymentService paymentService { get; set; }
        public TimerService timerService { get; set; }
        public MainWindow mainWindow { get; set; }
        
        
    }

}
