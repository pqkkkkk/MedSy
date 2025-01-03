using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using MedSy.ViewModels;
using MedSy.Views.User;
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

    public sealed partial class UserDashboard : Page
    {   
        public MainPageViewModel mainPageViewModel;
        public UserDashboard()
        {
            this.InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter != null)
            {
                var data = e.Parameter as MainPageViewModel;
                mainPageViewModel = data;
            }
        }

        private void OnlineConsultation_Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageViewModel.updateSelectedPage("Consultation");
            Frame.Navigate(typeof(Doctor_Infor));
        }

        private void MyConsultation_Click(object sender, RoutedEventArgs e)
        {
            mainPageViewModel.updateSelectedPage("MyConsultation");
            Frame.Navigate(typeof(MyConsultationsPage));
        }

        private void PrescriptionPaymentPage_Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageViewModel.updateSelectedPage("Pharmacy");
            Frame.Navigate(typeof(PrescriptionPaymentPage));
        }

        private void ChatWithDoctor_Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageViewModel.updateSelectedPage("Chat");
            Frame.Navigate(typeof(UserChatPage));
        }

        private void Pharmacy_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pharmacy));
        }
    }
}
