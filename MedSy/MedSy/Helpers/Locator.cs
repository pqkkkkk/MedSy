using MedSy.Services;
using MedSy.Services.Doctor;
using MedSy.Services.Management;
using MedSy.Services.Message;
using MedSy.Services.Patient;
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
        public IPatientDao patientDao { get; set; }
        public IDoctorDao doctorDao { get; set; }
        public IMessageDao messageDao { get; set; }
        public IManagementDao managementDao { get; set; }
        public SocketService socketService { get; set; }
       
        public MainWindow mainWindow { get; set; }

        public Locator()
        {
            managementDao = new ManagementSqlDao();
            patientDao = new PatientMockDao();
            doctorDao = new DoctorMockDao();
            messageDao = new MessageSqlDao();
            socketService = new SocketService();
            mainWindow = new MainWindow();
            
        }
    }
   
}
