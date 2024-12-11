using CommunityToolkit.WinUI.UI.Controls;
using MedSy.Models;
using MedSy.Services.User;
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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views.Doctor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrescriptionPage : Page
    {
        PresciptionPageViewModel prescriptionPageViewModel { get; set; }
        public PrescriptionPage()
        {
            this.InitializeComponent();
            prescriptionPageViewModel = new PresciptionPageViewModel();
            
            this.DataContext = prescriptionPageViewModel;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if(e.Parameter is Models.Consultation consultation)
            {
                prescriptionPageViewModel.selectedConsultation = consultation;
                IUserDao userDao = (Application.Current as App).locator.userDao;
                prescriptionPageViewModel.selectedUser = userDao.getUserById(consultation.patientId);
                prescriptionPageViewModel.LoadPrescriptionDetails();
                prescriptionPageViewModel.LoadDrugOfCorrespondingPrescriptionDetail();
            }
        }
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            prescriptionPageViewModel.search();
        }
        private void TypeSortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            prescriptionPageViewModel.LoadData();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            if (prescriptionPageViewModel.prescribed == true)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "You prescribed for this patient. Cannot edit",
                    CloseButtonText = "OK"
                }.ShowAsync();
                return;
            }

            if (prescriptionPageViewModel.selectedDrug == null)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "please choose which drug you want to add",
                    CloseButtonText = "OK"
                }.ShowAsync();
                return;
            }

            if (prescriptionPageViewModel.prescriptionDetails.FirstOrDefault(prescriptionDetail => prescriptionDetail.drug.name == prescriptionPageViewModel.selectedDrug.name) != null)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "You have already add this drug into prescription",
                    CloseButtonText = "OK"
                }.ShowAsync();
                return;
            }
            prescriptionPageViewModel.selectedDrug.quantity--;
            prescriptionPageViewModel.AddIntoSelectedDrugs();
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if(prescriptionPageViewModel.prescribed == true)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "You prescribed for this patient. Cannot edit",
                    CloseButtonText = "OK"
                }.ShowAsync();
                return;
            }
            int res = prescriptionPageViewModel.createPrescriptionAndUpdateQuantity();
            if (res == 1)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "prescribe successfully!",
                    CloseButtonText = "OK"
                }.ShowAsync();

                Frame.Navigate(typeof(PatientManagementPage));
            }
            else if (res == -1)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "There is nothing to save!",
                    CloseButtonText = "OK"
                }.ShowAsync();

            }
            else if(res == 0)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "There is something wrong in saving, try again!",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }

        private async void minus_Click(object sender, RoutedEventArgs e)
        {
            if (prescriptionPageViewModel.prescribed == true)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "You prescribed for this patient. Cannot edit",
                    CloseButtonText = "OK"
                }.ShowAsync();
                return;
            }
            if (!prescriptionPageViewModel.minus_click())
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "please choose drug you want to adjust",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }

        private async void plus_Click(object sender, RoutedEventArgs e)
        {
            if (prescriptionPageViewModel.prescribed == true)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "You prescribed for this patient. Cannot edit",
                    CloseButtonText = "OK"
                }.ShowAsync();
                return;
            }
            if (!prescriptionPageViewModel.plus_click())
            {
                if (prescriptionPageViewModel.selectedDrug.quantity == 0)
                {
                    await new ContentDialog()
                    {
                        XamlRoot = this.Content.XamlRoot,
                        Content = "out of stock",
                        CloseButtonText = "OK"
                    }.ShowAsync();
                }
                else
                {
                    await new ContentDialog()
                    {
                        XamlRoot = this.Content.XamlRoot,
                        Content = "please choose drug you want to adjust",
                        CloseButtonText = "OK"
                    }.ShowAsync();
                }
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PatientManagementPage));
        }

        private void IndicationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.DataContext is Drug drug)
            {
                string inputText = textBox.Text;

                prescriptionPageViewModel.SaveIndicationForDrug(drug, inputText);
            }
        }
    }
}