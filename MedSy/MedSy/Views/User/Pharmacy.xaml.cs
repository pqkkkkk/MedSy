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
    public sealed partial class Pharmacy : Page
    {
        public PharmacyViewModel pharmacyViewModel { get; set; }
        public Pharmacy()
        {
            this.InitializeComponent();
            pharmacyViewModel = new PharmacyViewModel();
            this.DataContext = pharmacyViewModel;
            //PageComboBox.SelectedIndex = 1; // Pharmacy

        }

        private async void Control2_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (!pharmacyViewModel.search())
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "Min price must be smaller than max price",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }

        private void Control2_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var selectedItem = args.SelectedItem as Drug; // giả sử là model thuốc
            pharmacyViewModel.selectedDrug = selectedItem;
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = pharmacyViewModel.GetSuggestions(sender.Text);
                sender.ItemsSource = suggestions;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                int selectedIndex = comboBox.SelectedIndex;

                if (selectedIndex == 0)
                {
                    Frame.Navigate(typeof(PrescriptionPaymentPage));
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            pharmacyViewModel.GoToPreviousPage();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            pharmacyViewModel.GoToNextPage();
        }

        private void pagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pagesComboBox.SelectedIndex >= 0)
            {
                var item = pagesComboBox.SelectedItem as PageInfo;
                pharmacyViewModel.GoToPage(item.Page);
            }
        }
    }
}
