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
using MedSy.Views.User;
using MedSy.Services.User;
using MedSy.Services;
using MedSy.ViewModels;
using MedSy.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            this.InitializeComponent();
        }

        private void SignUpUserControl_LetStartClicked(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private async void onSignUpButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (!RegexExpressionCheck.IsValidRegisterPassword(passwordBox.Password))
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "Password must have at least 6 characters, 1 number \n and not have special character ",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
            else if(!RegexExpressionCheck.IsValidUsername(usernameBox.Text))
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "Username must have at least 6 characters \nand not special character",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
            else if(role.SelectedItem == null)
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "please choose your role",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
            else
            {
                // Ẩn giao diện đăng ký
                SignUp_part.Visibility = Visibility.Collapsed;
                // Hiển thị UserControl
                DetailsUserControl.Visibility = Visibility.Visible;

                var selectedRole = (role.SelectedItem as ComboBoxItem)?.Content.ToString();
                var password = passwordBox.Password;
                var username = usernameBox.Text;
                    // Gắn sự kiện Back và sự kiện LetStart cho UserControl
                    if (DetailsUserControl is SignUpUserControl userControl)
                    {
                        userControl.SetPassword(password);
                        userControl.SetRole(selectedRole);
                        userControl.SetUsername(username);
                        userControl.BackClicked += UserControl_BackClicked;
                        userControl.LetStartClicked += SignUpUserControl_LetStartClicked;

                    }
                }
        }
        private void UserControl_BackClicked(object sender, EventArgs e)
        {
            // Hiển thị lại giao diện đăng ký
            SignUp_part.Visibility = Visibility.Visible;

            // Ẩn UserControl
            DetailsUserControl.Visibility = Visibility.Collapsed;
        }

        private void SignInClicked_hyperlinkButton(object sender, RoutedEventArgs e)
        {   

            var signinWindow = new SignInWindow();
            signinWindow.Activate();
            this.Close();
        }
    }
}
