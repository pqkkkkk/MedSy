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
using MedSy.Models;
using MedSy.Views.User;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Doctor_Infor : Page
    {
        public DoctorViewModel DoctorViewModel { get; set; }
        public Doctor_Infor()
        {
            this.InitializeComponent();
            DoctorViewModel = new DoctorViewModel();
            this.DataContext = DoctorViewModel;
        }

        private void Doctor_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Models.Doctor doctor)
            {
                DoctorViewModel.SelectedDoctor = doctor;
                DoctorViewModel.LoadFeedback();
                Frame.Navigate(typeof(DoctorDetail), DoctorViewModel);
            }
        }

        private async void Control2_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            DoctorViewModel.Search();
        }

        private void Control2_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var selectedItem = args.SelectedItem as Models.Doctor; 
            DoctorViewModel.SelectedDoctor = selectedItem;
            DoctorViewModel.Search();
        }
        
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = DoctorViewModel.GetSuggestions(sender.Text);
                sender.ItemsSource = suggestions;
            }
        }
        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorViewModel.GoToPreviousPage();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorViewModel.GoToNextPage();
        }

        private void pagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pagesComboBox.SelectedIndex >= 0)
            {
                var item = pagesComboBox.SelectedItem as PageInfo;
                DoctorViewModel.GoToPage(item.Page);
            }
        }

        private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var selectedDoctor = DoctorViewModel.SelectedDoctor;
            if (selectedDoctor != null)
            {

            };
        }

        private void OnSecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var selectedDoctor = DoctorViewModel.SelectedDoctor;
            if (selectedDoctor != null)
            {
                
            };
        }

        private void RefreshAll(object sender, RoutedEventArgs e)
        {
            DoctorViewModel.resetsort();
        }
    }
}
