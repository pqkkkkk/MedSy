using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace MedSy.Views.Admin
{
    public sealed partial class AdminDashboard : Page
    {
        public AdminDashboard()
        {
            this.InitializeComponent();
            contentFrame.Navigate(typeof(DrugManagementPage));
        }
        private void NavigateToPage(string tag)
        {
            switch (tag)
            {
                case "DrugManagement":
                    contentFrame.Navigate(typeof(DrugManagementPage));
                    break;
                case "StatisticPage":
                    contentFrame.Navigate(typeof(StatisticPage));
                    break;
            }
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = sender.SelectedItem as NavigationViewItem;
            if (selectedItem != null)
            {
                string tag = selectedItem.Tag.ToString();
                NavigateToPage(tag);
            }
        }
    }
}
