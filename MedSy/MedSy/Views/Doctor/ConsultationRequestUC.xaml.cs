using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MedSy.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views.Doctor
{
    public sealed partial class ConsultationRequestUC : UserControl
    {
        public delegate void EventHandler();
        public event EventHandler CreateRoomClickedEvent;

        public ConsultationRequestsViewModel consultationRequestsViewModel { get; set; }
        public ConsultationRequestUC(ConsultationRequestsViewModel consultationRequestsViewModel)
        {
            this.InitializeComponent();
            this.consultationRequestsViewModel = consultationRequestsViewModel;
            this.DataContext = consultationRequestsViewModel;
            setupUI();
        }
        private void setupUI()
        {
            if (consultationRequestsViewModel.consultations.Count == 0)
            {
                emptyUCListMessage.Visibility = Visibility.Visible;
                searchField.Visibility = Visibility.Collapsed;
                mainField.Visibility = Visibility.Collapsed;
            }
            else
            {
                emptyUCListMessage.Visibility = Visibility.Collapsed;
                searchField.Visibility = Visibility.Visible;
                mainField.Visibility = Visibility.Visible;
                addItemsToTimeFilter();
            }
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
        private async void acceptAllClicked(object sender, RoutedEventArgs e)
        {
            int result = consultationRequestsViewModel.acceptRequest();
            if (result == -1)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Title = "Unable to accept request",
                    Content = "There is a consultation at the same time",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }
        private void rejectAllClicked(object sender, RoutedEventArgs e)
        {
            consultationRequestsViewModel.rejectRequest();
        }
        private void cancelRequestClicked(object sender, RoutedEventArgs e)
        {
            consultationRequestsViewModel.cancelRequest();
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
        private async void joinRoomClicked(object sender, RoutedEventArgs e)
        {
            if (consultationRequestsViewModel.nextConsultationToday == null)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Title = "Unable join room",
                    Content = "There is no next consultation",
                    CloseButtonText = "OK"
                }.ShowAsync();

                return;
            }
            else
            {
                TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
                TimeOnly startTime = new TimeOnly(0, 0, 0);
                TimeOnly endTime = new TimeOnly(23, 59, 59);
                TimeSpan timeDiff = startTime.ToTimeSpan().Subtract(now.ToTimeSpan());
                startTime = consultationRequestsViewModel.nextConsultationToday.startTime;
                endTime = consultationRequestsViewModel.nextConsultationToday.endTime;
                timeDiff = startTime.ToTimeSpan().Subtract(now.ToTimeSpan());
            
            
                if (timeDiff.TotalMinutes > 10)
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
                if(endTime < now)
                {
                    var dialog = new ContentDialog()
                    {
                        XamlRoot = this.Content.XamlRoot,
                        Title = "Too Late",
                        Content = "You can only join the room within the consultation time.",
                        CloseButtonText = "Close and refresh page",

                    };
                    dialog.Closed += CloseTooLateDialog;
                    await dialog.ShowAsync();
                    return;
                }
            }
            CreateRoomClickedEvent?.Invoke();
        }
        private void CloseTooLateDialog(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            consultationRequestsViewModel.updateAllMissedConsultations();
            consultationRequestsViewModel.getConsultations(null, null, null);
            consultationRequestsViewModel.getNextConsultationTodayInfo();
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
        private void RefreshAll(object sender, RoutedEventArgs e)
        {
            consultationRequestsViewModel.updateAllMissedConsultations();
            consultationRequestsViewModel.getConsultations(null, null, null);
            consultationRequestsViewModel.getNextConsultationTodayInfo();
        }
    }
}
