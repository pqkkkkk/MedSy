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
        private void EditPrescription_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var consultationItem = button.DataContext as Consultation;

            if (consultationItem != null)
            {
                Debug.WriteLine($"Consultation ID: {consultationItem.id}");
            }

            Frame.Navigate(typeof(PrescriptionPage), consultationItem );
        }
        private void OnSelectedPatientItemChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = patientListView.SelectedItem as Models.PatientManagementItem;
            patientManagementViewModel.UpdateSelectedPatientItem(item);
        }
    }
}
