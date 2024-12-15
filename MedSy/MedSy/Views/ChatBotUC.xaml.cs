using MedSy.Models;
using MedSy.ViewModels;
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
    public sealed partial class ChatBotUC : UserControl
    {
        public ChatBotViewModel chatBotViewModel { get; set; }
        public ChatBotUC()
        {
            this.InitializeComponent();
            chatBotViewModel = new ChatBotViewModel();
        }

        private void sendMessageClick(object sender, RoutedEventArgs e)
        {
           
        }
        public void HasNewCRHandler(int value)
        {
            chatBotViewModel.HasNewCR = value;
            chatBotViewModel.addNewCRNotification();
        }
        public void HasNewConsultationTodayHandler(int value)
        {
            chatBotViewModel.HasConsultationToday = value;
            chatBotViewModel.addConsultationTodayNotification();
        }
    }
}
