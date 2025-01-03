using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using MedSy.ViewModels;
using MedSy.Views.Doctor;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DoctorDashboard : Page
    {
        public MainPageViewModel mainPageViewModel;
        public DoctorDashboard()
        {
            this.InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                var data = e.Parameter as MainPageViewModel;
                mainPageViewModel = data;
            }
        }

        private void ConsultationRequest_Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageViewModel.updateSelectedPage("ConsultationRequest");
            Frame.Navigate(typeof(ConsultationRequestsPage));
        }

        private void WorkSchedule_Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageViewModel.updateSelectedPage("workSchedule");
            Frame.Navigate(typeof(WorkSchedulePage));
        }

        private void PatientManagement_Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageViewModel.updateSelectedPage("patientManagement");
            Frame.Navigate(typeof(PatientManagementPage));
        }

        private void Chat_Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageViewModel.updateSelectedPage("Chat");
            Frame.Navigate(typeof(DoctorChatPage));
        }
    }
}
