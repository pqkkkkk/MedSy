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
    public sealed partial class ChatBotUC : UserControl
    {
        public ChatBotViewModel chatBotViewModel { get; set; }
        public delegate void ChatBotUpdateEventHandler();
        public event ChatBotUpdateEventHandler ChatBotUpdate;
        public ChatBotUC()
        {
            this.InitializeComponent();
            chatBotViewModel = new ChatBotViewModel();
            chatBotViewModel.ChatBotUpdate += ChatBotUpdateHandler;
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

        private async void SendQuestionClicked(object sender, RoutedEventArgs e)
        {
            var selectedQuestion = questionOptions.SelectedItem as Message;

            if(selectedQuestion == null)
            {
                await new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "Error",
                    Content = "Please select a question",
                    CloseButtonText = "Ok"
                }.ShowAsync();
                return;
            }
            string selectedQuestionContent = selectedQuestion.content;
            chatBotViewModel.GetAnswer(selectedQuestionContent);

        }
        private void RefreshConversationClicked(object sender, RoutedEventArgs e)
        {
            chatBotViewModel.messageList.Clear();
            questionOptions.SelectedItem = null;
        }
        private void ChatBotUpdateHandler()
        {
            (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                ChatBotUpdate?.Invoke();
            });
        }
    }
}
