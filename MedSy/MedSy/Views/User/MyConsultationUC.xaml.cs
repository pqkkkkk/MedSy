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

namespace MedSy.Views.User
{
    public sealed partial class MyConsultationUC : UserControl
    {
        public delegate void EventHandler();
        public event EventHandler CreateRoomClickedEvent;

        public ConsultationRequestsViewModel consultationRequestsViewModel { get; set; }
        public MyConsultationUC(ConsultationRequestsViewModel consultationRequestsViewModel)
        {
            this.InitializeComponent();
            this.consultationRequestsViewModel = consultationRequestsViewModel;
            this.DataContext = consultationRequestsViewModel;
            addItemsToTimeFilter();
        }
        private void addItemsToTimeFilter()
        {
            for (int i = 0; i <= 23; i++) 
            {
                ComboBoxItem item1 = new ComboBoxItem
                {
                    Content = $"{i}"
                };
                ComboBoxItem item2 = new ComboBoxItem
                {
                    Content = $"{i}"
                };
                endHour.Items.Add(item1);
                startHour.Items.Add(item2);
                startHour.SelectedItem = startHour.Items[0];
                endHour.SelectedItem = endHour.Items[23];
            }

            for (int i = 0; i <= 59; i++)
            {
                ComboBoxItem item1 = new ComboBoxItem
                {
                    Content = $"{i}"
                };
                ComboBoxItem item2 = new ComboBoxItem
                {
                    Content = $"{i}"
                };
                endMinute.Items.Add(item1);
                startMinute.Items.Add(item2);
                startMinute.SelectedItem = startMinute.Items[0];
                endMinute.SelectedItem = endMinute.Items[59];
            }

        }
        private async void searchClicked(object sender, RoutedEventArgs e)
        {
            int startHourValue = int.Parse((startHour.SelectedItem as ComboBoxItem).Content as string);
            int startMinuteValue = int.Parse((startMinute.SelectedItem as ComboBoxItem).Content as string);

            TimeOnly startTime = new TimeOnly(startHourValue, startMinuteValue, 0);
            int endHourValue = int.Parse((endHour.SelectedItem as ComboBoxItem).Content as string);
            int endMinuteValue = int.Parse((endMinute.SelectedItem as ComboBoxItem).Content as string);
            TimeOnly endTime = new TimeOnly(endHourValue, endMinuteValue, 0);

            if (startTime > endTime)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Title = "Invalid Time",
                    Content = "Start time (From) should be less than end time (To). Please try again",
                    CloseButtonText = "OK"
                }.ShowAsync();

                return;
            }
            DateTimeOffset? selectedDateTime = dateFilter.Date;
            DateOnly? date = null;
            if (selectedDateTime.HasValue)
            {
                date = DateOnly.FromDateTime(selectedDateTime.Value.DateTime);
            }
            
            consultationRequestsViewModel.searchAndFilterConsultations(date, startTime, endTime);

        }
        private void cancelRequestClicked(object sender, RoutedEventArgs e)
        {
            consultationRequestsViewModel.cancelRequest();
        }
        private async void joinRoomClicked(object sender, RoutedEventArgs e)
        {
            TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
            TimeOnly startTime = new TimeOnly(0, 0, 0);
            TimeSpan timeDiff = startTime.ToTimeSpan().Subtract(now.ToTimeSpan());

            if (consultationRequestsViewModel.nextConsultationToday != null)
            {
                startTime = consultationRequestsViewModel.nextConsultationToday.startTime;
                timeDiff = startTime.ToTimeSpan().Subtract(now.ToTimeSpan());
            }

            if (consultationRequestsViewModel.nextConsultationToday == null)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Title ="Unable join room",
                    Content = "There is no next consultation",
                    CloseButtonText = "OK"
                }.ShowAsync();

                return;
            }
            else if(timeDiff.TotalMinutes > 10)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Title = "Too Early",
                    Content = "You can only join the room within 10 minutes of the start time.",
                    CloseButtonText = "OK"
                }.ShowAsync();

                return;
            }
            CreateRoomClickedEvent?.Invoke();
        }
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Models.Consultation consultation = requestList.SelectedItem as Models.Consultation;
            consultationRequestsViewModel.updateSelectedConsultation(consultation);
        }
        private void selectStatusClicked(object sender, RoutedEventArgs e)
        {
            string status = (sender as Button).Tag as string;
            consultationRequestsViewModel.updateSelectedStatus(status);
            consultationRequestsViewModel.getConsultations(null, null, null);
        }
        private void refreshFilterClicked(object sender, RoutedEventArgs e)
        {
            startHour.SelectedItem = startHour.Items[0];
            endHour.SelectedItem = endHour.Items[23];
            startMinute.SelectedItem = startMinute.Items[0];
            endMinute.SelectedItem = endMinute.Items[59];
            dateFilter.PlaceholderText = "select a date";
            dateFilter.Date = null;
        }
    }
}
