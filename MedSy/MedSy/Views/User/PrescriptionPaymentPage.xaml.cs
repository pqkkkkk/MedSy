using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using MedSy.ViewModels;
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

namespace MedSy.Views.User
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrescriptionPaymentPage : Page
    {
        public PrescriptionPaymentViewModel prescriptionPaymentViewModel { get; set; }
        public PrescriptionPaymentPage()
        {
            this.InitializeComponent();
            prescriptionPaymentViewModel = new PrescriptionPaymentViewModel();
            this.DataContext = prescriptionPaymentViewModel;
            PageComboBox.SelectedIndex = 0; // MyPrecsription

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                int selectedIndex = comboBox.SelectedIndex;

                if (selectedIndex == 1)
                {
                    Frame.Navigate(typeof(Pharmacy));
                }
            }
        }

        private void Prescription_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                prescriptionPaymentViewModel.selectedPrescription = comboBox.SelectedItem as Models.Prescription;
                prescriptionPaymentViewModel.LoadPrescriptionDetails();
            }
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
