using MedSy.Services;
using MedSy.ViewModels;
using MedSy.Views.User;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views
{
    public sealed partial class UserMainPage : Page
    {
        private MainPageViewModel mainPageViewModel;
        public UserMainPage()
        {
            this.InitializeComponent();
            content.Navigate(typeof(UserDashboard));
            
            mainPageViewModel = new MainPageViewModel();
        }
        private void UserProfileClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(UserDashboard));
        }

        private void DashboardCLick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(UserDashboard));
        }

        private void ConsultationClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(Doctor_Infor));
        }

        private void ChatClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            mainPageViewModel.offNewMessageNotification();
            content.Navigate(typeof(UserChatPage));
        }

        private void PharmacyClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(Pharmacy));
        }

        private void MedicalNewsClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(UserDashboard));
        }

        private void MyConsultationClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(MyConsultationsPage));
        }
    }
}
