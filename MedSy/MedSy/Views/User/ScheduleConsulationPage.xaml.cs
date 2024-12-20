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
    public sealed partial class ScheduleConsulationPage : Page
    {
        public ScheduleConsulationViewModel scheduleConsulationViewModel { get; set; }
        Models.Doctor selectedDoctor { get; set; }
        public ScheduleConsulationPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Doctor_Infor));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            selectedDoctor = e.Parameter as Models.Doctor;
            scheduleConsulationViewModel = new ScheduleConsulationViewModel(selectedDoctor);
            this.DataContext = scheduleConsulationViewModel;
        }

        private void CalendarDatePicker_SelectedDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs e)
        {
            if (e.NewDate.HasValue)
            {
                // Lấy ngày được chọn từ CalendarDatePicker
                var selectedDate = e.NewDate.Value;

                // Định dạng lại ngày theo định dạng dd/MM/yyyy
                string formattedDate = selectedDate.ToString("dd/MM/yyyy");

                System.Diagnostics.Debug.WriteLine(formattedDate);  // Ghi ngày đã định dạng vào Debug Output
            }
        }

        private async void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if(formComboBox.SelectedItem == null) {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "please choose your consultation form",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
            else if(ConsulationDate.Date == null)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "please choose your consultation day",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
            else if (pathologyComboBox.SelectedItem == null)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "please choose your pathology",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
            else if (scheduleConsulationViewModel.CreateConsultation())
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "Consultation was scheduled Successfully",
                    CloseButtonText = "OK"
                }.ShowAsync();
                Frame.GoBack();
            }
            else
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "An error occurred during processing, please try again.",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }

        private void selectedTime_Changed(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            scheduleConsulationViewModel.selected_endTime = scheduleConsulationViewModel.selected_startTime.Add(TimeSpan.FromHours(1));
        }
    }

}
