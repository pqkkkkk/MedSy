using MedSy.Helpers;
using MedSy.Services.Management;
using MedSy.Services.Message;
using MedSy.Services;
using MedSy.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using MedSy.Services.User;
using MedSy.Services.Consultation;
using MedSy.Services.Drug;
using MedSy.Services.Prescription;
using Microsoft.Data.SqlClient;
using System.Diagnostics;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy
{
    
    public partial class App : Application
    {
        public Locator locator { get; set; }
        private SignInWindow signInWindow;
        public App()
        {
            this.InitializeComponent();
        }

       
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            locator = new Locator();
            (Application.Current as App).locator.databaseConnectionString = """
                Server = localhost;
                Database = medsytest;
                User ID = sa;
                Password = SqlServer@123;
                TrustServerCertificate = True;
                """;
            (Application.Current as App).locator.sqlConnection = new SqlConnection((Application.Current as App).locator.databaseConnectionString);
            Debug.WriteLine("Connect to database successfully");
            (Application.Current as App).locator.userDao = new UserSqlDao();
            (Application.Current as App).locator.managementDao = new ManagementSqlDao();
            (Application.Current as App).locator.messageDao = new MessageSqlDao();
            (Application.Current as App).locator.consultationDao = new ConsultationSqlDao();
            (Application.Current as App).locator.socketService = new SocketService();
            (Application.Current as App).locator.chatBotService = new ChatBotService();
            (Application.Current as App).locator.paymentService = new PaymentService();
            (Application.Current as App).locator.timerService = new TimerService();
            (Application.Current as App).locator.drugDao = new DrugSqlDao();
            (Application.Current as App).locator.prescriptionDao = new PrescriptionSqlDao();
            signInWindow = new SignInWindow();
            signInWindow.Activate();
        }

        
    }
}
