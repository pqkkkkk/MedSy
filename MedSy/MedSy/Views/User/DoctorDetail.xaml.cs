using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using MedSy.Models;
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
    public sealed partial class DoctorDetail : Page
    {
        public DoctorViewModel DoctorViewModel { get; set; }

        public DoctorDetail()
        {
            this.InitializeComponent();
            DoctorViewModel = new DoctorViewModel();
            this.DataContext = DoctorViewModel;
        }

        private void BackDoctorInfor_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Doctor_Infor));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is DoctorViewModel doctorViewModel)
            {
                DoctorViewModel = doctorViewModel;
                this.DataContext = DoctorViewModel;
            }
            base.OnNavigatedTo(e);
        }

        private async void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorViewModel.SelectedDoctor != null)
            {
                if (string.IsNullOrWhiteSpace(CommentBox.Text) && CommentRating.Value <= 0)
                {
                    await MissingInfoDialog.ShowAsync();
                }
                else if (!string.IsNullOrWhiteSpace(CommentBox.Text) && CommentRating.Value <= 0)
                {
                    await MissingInfoDialog.ShowAsync();
                }
                else
                {
                    var newFeedback = new Feedback
                    {
                        PatientID = (Application.Current as App).locator.currentUser.id,
                        DoctorID = DoctorViewModel.SelectedDoctor.id,
                        Content = CommentBox.Text,
                        Rating = (int)CommentRating.Value
                    };

                    DoctorViewModel.AddFeedback(newFeedback);

                    CommentBox.Text = string.Empty;
                    CommentRating.Value = 0;

                }
            }
        }


        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ScheduleConsulationPage), DoctorViewModel.SelectedDoctor);
        }
        
        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

