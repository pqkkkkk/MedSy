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
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views.Admin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DrugManagementPage : Page
    {
        public DrugManagementViewModel drugManagementViewModel { get; set; }
        public DrugManagementPage()
        {
            this.InitializeComponent();
            drugManagementViewModel = new DrugManagementViewModel();
            this.DataContext = drugManagementViewModel;

        }

        private async void AddDrug_ButtonClick(object sender, RoutedEventArgs e)
        {
            await AddDrugDialog.ShowAsync();
        }
        private async void AddDrugDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            int price = 0;
            int quantity = 0;

            if (string.IsNullOrEmpty(DrugNameTextBox.Text))
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Drug name cannot be empty.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddDrugDialog.ShowAsync();
                return;
            }
            else if (string.IsNullOrEmpty(DrugPriceTextBox.Text) || !int.TryParse(DrugPriceTextBox.Text, out price))
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Invalid price value.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddDrugDialog.ShowAsync();
                return;
            }
            else if (string.IsNullOrEmpty(DrugQuantityTextBox.Text) || !int.TryParse(DrugQuantityTextBox.Text, out quantity))
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Invalid quantity value.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddDrugDialog.ShowAsync();
                return;
            }
            else if (string.IsNullOrEmpty(DrugUnitTextBox.Text))
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Drug unit cannot be empty.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddDrugDialog.ShowAsync();
                return;
            }
            else if (!ManufacturingDatePicker.SelectedDate.HasValue || !ExpiryDatePicker.SelectedDate.HasValue)
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Please select both manufacturing and expiry dates.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddDrugDialog.ShowAsync();
                return;
            }
            else if(ManufacturingDatePicker.Date.DateTime > ExpiryDatePicker.Date.DateTime)
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Expiry date must be after manufacturing date.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddDrugDialog.ShowAsync();
                return;
            }
            else if(price <= 0 || quantity <= 0)
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Price and quantity must be greater than 0.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddDrugDialog.ShowAsync();
                return;
            }
            else if (drugManagementViewModel.drugs.Any(d => d.name == DrugNameTextBox.Text))
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Drug name already exists.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddDrugDialog.ShowAsync();
                return;
            }
            else if(string.IsNullOrEmpty(DrugTypeTextBox.Text))
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Drug type cannot be empty.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddDrugDialog.ShowAsync();
                return;
            }

            Drug newDrug = new Drug
            {
                name = DrugNameTextBox.Text,
                price = price,
                quantity = quantity,
                unit = DrugUnitTextBox.Text,
                manufacturing_date = DateOnly.FromDateTime(ManufacturingDatePicker.Date.DateTime),
                expiry_date = DateOnly.FromDateTime(ExpiryDatePicker.Date.DateTime),
                drugTypeName = DrugTypeTextBox.Text
            };

            if (drugManagementViewModel.addDrug(newDrug))
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Success",
                    Content = "Drug added successfully.",
                    CloseButtonText = "Ok",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();

                DrugNameTextBox.Text = "";
                DrugPriceTextBox.Text = "";
                DrugQuantityTextBox.Text = "";
                DrugUnitTextBox.Text = "";
                ManufacturingDatePicker.Date = DateTimeOffset.Now;
                ExpiryDatePicker.Date = DateTimeOffset.Now;
                DrugTypeTextBox.Text = "";
                return;
            }
            else
            {
                AddDrugDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Failed to add drug. Please try again.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddDrugDialog.ShowAsync();
                return;
            }
        }

        private void AddDrugDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Xử lý khi người dùng nhấn nút "Cancel"
        }

        private async void AddMoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (drugManagementViewModel.selectedDrug == null || DrugList.SelectedItem == null)
            {
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Please choose a drug first.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                return;
            }

            await AddQuantityDialog.ShowAsync();
        }
        private async void Control2_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            drugManagementViewModel.Search();
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = drugManagementViewModel.GetSuggestions(sender.Text);
                sender.ItemsSource = suggestions;
            }
        }
        private void Control2_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            string selectedDrugName = args.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedDrugName))
            {
                drugManagementViewModel.Keyword = selectedDrugName;
                drugManagementViewModel.Search();
            }
        }

        private void Back_ButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            drugManagementViewModel.GoToPreviousPage();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            drugManagementViewModel.GoToNextPage();
        }

        private void pagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pagesComboBox.SelectedIndex >= 0)
            {
                var item = pagesComboBox.SelectedItem as PageInfo;
                drugManagementViewModel.GoToPage(item.Page);
            }
        }

        private async void AddQuantityDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            int quantityToAdd = 0;
            if(int.TryParse(AddQuantityTextBox.Text, out quantityToAdd) && quantityToAdd>=0)
            {
                drugManagementViewModel.updateQuantity(quantityToAdd);
                AddQuantityTextBox.Text = "";
            }
            else
            {
                AddQuantityDialog.Hide();
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Invalid quantity input!",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();
                await AddQuantityDialog.ShowAsync();
                return;
            }
        }

        private void AddQuantityDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
