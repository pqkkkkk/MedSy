using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using MedSy.Services.User;
using MedSy.Services;
using MedSy.ViewModels;
using MedSy.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views.User
{
    public sealed partial class SignUpUserControl : UserControl
    {
        public event EventHandler BackClicked;
        public event EventHandler LetStartClicked;
        public event EventHandler UserSignedUp;
        private string _role;
        private string _password;
        private string _username;
        public SignUpUserControl()
        {
            this.InitializeComponent();

        }

        // Phương thức để set giá trị role
        public void SetRole(string role)
        {
            _role = role;
        }

        public void SetPassword(string pw)
        {
            _password = pw;
        }

        public void SetUsername(string us)
        {
            _username = us;
        }
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            BackClicked?.Invoke(this, EventArgs.Empty);
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

        private async void Signup_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra dữ liệu từ các ô nhập liệu
            if (!RegexExpressionCheck.IsValidEmail(EmailBox.Text))
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "email must have format: something@gmail.com",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
            else if (!RegexExpressionCheck.IsValidPhoneNumber(PhoneNumberBox.Text))
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "Phone number must have 10 numbers",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(FullnameBox.Text) ||
                GenderBox.SelectedItem == null ||
                BirthdayBox.Date == null)
            {
                await MissingInfo.ShowAsync();

            }
            else
            {
                await Signup_success.ShowAsync();
            }
        }

        private void Signup_success_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var selectedGender = (GenderBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var newUser = new Models.User
            {
                username = _username,
                password = _password,
                id = (Application.Current as App).locator.users.Count + 1,
                fullName = FullnameBox.Text,
                email = EmailBox.Text,
                phoneNumber = PhoneNumberBox.Text,
                gender = selectedGender,
                birthday = BirthdayBox.Date.Value.DateTime,
                role = _role,  // Sử dụng _role đã được set từ SignUpWindow
            };

            (Application.Current as App).locator.users.Add(newUser);

            var signinWindow = new SignInWindow();
            signinWindow.Activate();
            LetStartClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
