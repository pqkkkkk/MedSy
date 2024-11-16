using MedSy.Models;
using MedSy.ViewModels;
using Microsoft.UI;
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
    public sealed partial class ConsultationRequestsPage : Page
    {
        public ConsultationRequestsViewModel consultationRequestsViewModel { get; set; }
        public ConsultationRequestsPage()
        {
            this.InitializeComponent();
            consultationRequestsViewModel = new ConsultationRequestsViewModel();
            this.DataContext = consultationRequestsViewModel;
        }
        private void searchClicked(object sender, RoutedEventArgs e)
        {
            
            DateTimeOffset? selectedDate = dateFilter.Date;
            DateTime? date = null;
            if (selectedDate.HasValue)
            {
                date = selectedDate.Value.DateTime;
            }

            consultationRequestsViewModel.searchAndFilterConsultations(date);

        }
        private void acceptAllClicked(object sender, RoutedEventArgs e)
        {
            consultationRequestsViewModel.acceptRequest();   
        }

        private void rejectAllClicked(object sender, RoutedEventArgs e)
        {
            consultationRequestsViewModel.rejectRequest();
        }
        private void cancelRequestClicked(object sender, RoutedEventArgs e)
        {
            
            
            consultationRequestsViewModel.cancelRequest();
        }

        private void createRoomClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Models.Consultation consultation = requestList.SelectedItem as Models.Consultation;
            consultationRequestsViewModel.updateSelectedConsultation(consultation);
        }

        private void selectStatusClicked(object sender, RoutedEventArgs e)
        {
            string status = (sender as Button).Tag as string;
            consultationRequestsViewModel.getConsultations(status, null);
            consultationRequestsViewModel.updateSelectedStatus(status);
        }
    }
}
