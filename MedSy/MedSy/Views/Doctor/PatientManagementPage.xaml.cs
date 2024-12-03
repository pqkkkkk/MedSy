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

namespace MedSy.Views.Doctor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PatientManagementPage : Page
    {
        public PatientManagementViewModel patientManagementViewModel { get; set; }
        public PatientManagementPage()
        {
            this.InitializeComponent();
            patientManagementViewModel = new PatientManagementViewModel();
        }

        private void OnPatientItemTapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is Models.PatientManagementItem item)
            {
                item.isExpanded = !item.isExpanded;
            }
        }

        private void OnConsultationTapped(object sender, TappedRoutedEventArgs e)
        {
            string consultationIdString = (sender as StackPanel).Tag.ToString();
            int consultationId = int.Parse(consultationIdString);
            patientManagementViewModel.UpdateSelectedConsultation(consultationId);
        }
    }
}
