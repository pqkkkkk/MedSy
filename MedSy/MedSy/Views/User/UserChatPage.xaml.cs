using MedSy.Models;
using MedSy.Services;
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

namespace MedSy.Views.User
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserChatPage : Page
    {
        public ChatViewModel chatViewModel { get; set; }
        public UserChatPage()
        {
            this.InitializeComponent();

            chatViewModel = new ChatViewModel();
        }
        private void switchToNewChatClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedUser = button?.Tag as Models.User;
            chatViewModel.updateSelectedUser(selectedUser);

            int currentUserId = (Application.Current as App).locator.currentUser.id;
            int oppositeUserId = chatViewModel.selectedUser.id;
            chatViewModel.loadMessages(currentUserId, oppositeUserId);
        }

        private async void SendMessageClick(object sender, RoutedEventArgs e)
        {
            string message = messageTextBox.Text;
            int senderId = (Application.Current as App).locator.currentUser.id;
            int receiverId = chatViewModel.selectedUser.id;

            await chatViewModel.sendMessage(message, senderId,receiverId);
            messageTextBox.Text = "";
            
        } 
    }
}
