using MedSy.ViewModels;
using MedSy.Views.Doctor;
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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DoctorMainPage : Page
    {
        private MainPageViewModel mainPageViewModel;
        public Frame contentFrame => content;
        public delegate void HasNewCREventHandler(int value);
        public event HasNewCREventHandler HasNewCR;
        public delegate void HasNewConsultationTodayEventHandler(int value);
        public event HasNewConsultationTodayEventHandler HasNewConsultationToday;
        public DoctorMainPage()
        {
            this.InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
            content.Navigate(typeof(DoctorDashboard),mainPageViewModel);
            HasNewConsultationToday += chatBot.HasNewConsultationTodayHandler;
            HasNewCR += chatBot.HasNewCRHandler;
            HasNewConsultationToday?.Invoke(mainPageViewModel.HasConsultationToday);
            HasNewCR?.Invoke(mainPageViewModel.HasNewCR);
        }
        
        private void UserProfileClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(DoctorDashboard));
        }
        private void DashboardCLick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(DoctorDashboard),mainPageViewModel);
        }
        private void ConsultationRequestClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(ConsultationRequestsPage));
        }
        private void ChatClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            mainPageViewModel.offNewMessageNotification();
            content.Navigate(typeof(DoctorChatPage));
        }
        private void workScheduleClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(WorkSchedulePage));
        }
        private void patientManagementClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string selectedPage = button.Tag.ToString();
            mainPageViewModel.updateSelectedPage(selectedPage);
            content.Navigate(typeof(PatientManagementPage));
        }

        private void ChatBotClicked(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                chatBotAnimation.AutoPlay = false;
                chatBotAnimation.Pause();
            });
            
        }

        private void ChatBotUpdateHandler()
        {
            (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                chatBotAnimation.AutoPlay = true;
                chatBotAnimation.Resume();
            });
        }
    }
}
