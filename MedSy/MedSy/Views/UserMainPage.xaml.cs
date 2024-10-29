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
        public UserMainPage()
        {
            this.InitializeComponent();
            content.Navigate(typeof(UserDashboard));
        }

        private void Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = args.SelectedItem as NavigationViewItem;
            string selectedPage = selectedItem.Tag.ToString();

           
            switch (selectedPage)
            {
                case "UserProfile":
                    break;
                case "Dashboard":
                    content.Navigate(typeof(UserDashboard));
                    break;
                case "Consultation":
                    break;
                case "Chat":
                    content.Navigate(typeof(UserChatPage));
                    break;
                case "Pharmacy":
                    break;
                case "MedicalNews":
                    break;
            }
          
        }
    }
}
