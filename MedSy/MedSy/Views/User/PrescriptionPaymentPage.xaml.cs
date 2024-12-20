using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using Windows.UI.Popups;

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
            (Application.Current as App).locator.socketService.paymentCompleteMessageReceived += PaymentCompleteHandler;

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

        private async void PayButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if(prescriptionPaymentViewModel.selectedPrescription == null)
            {
                await new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "No prescription",
                    Content = "Please select prescription before payment",
                    CloseButtonText = "OK"
                }.ShowAsync();
                return;
            }
            
            string paymentUrl = await (Application.Current as App).locator.paymentService.CreatePaymentAsync(prescriptionPaymentViewModel.selectedPrescription.totalPrice,prescriptionPaymentViewModel.selectedPrescription.prescriptionId);
            prescriptionField.Visibility = Visibility.Collapsed;
            paymentField.Source = new Uri(paymentUrl);
            paymentField.Visibility = Visibility.Visible;
        }
        private void PaymentCompleteHandler()
        {
            (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                prescriptionPaymentViewModel.UpdatePrescriptionStatus();
                prescriptionPaymentViewModel.LoadData();
                paymentField.Source = null;
                paymentField.Visibility = Visibility.Collapsed;
                prescriptionField.Visibility = Visibility.Visible;
            });
           
        }
    }
}
