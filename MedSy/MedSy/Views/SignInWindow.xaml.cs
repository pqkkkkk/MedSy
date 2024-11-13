using MedSy.Services.Management;
using MedSy.Services.Message;
using MedSy.Services.User;
using MedSy.Services;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            this.InitializeComponent();
        }

        private async void SignInClicked(object sender, RoutedEventArgs e)
        {
            var username = usernameBox.Text;
            var password = passwordBox.Password;

            
            Models.User user = (Application.Current as App).locator.userDao.getUserByUsername(username);
           
            if (user != null)
            {
                var actualUsername = user.username;
                var actualPassword = user.password;

                if (username == actualUsername && password == actualPassword)
                {
                    (Application.Current as App).locator.currentUser = user;

                    int userId = (Application.Current as App).locator.currentUser.id;
                    await (Application.Current as App).locator.socketService.connectAsync();
                    await (Application.Current as App).locator.socketService.register(userId);

                    string roleOfUser = (Application.Current as App).locator.currentUser.role;
                    (Application.Current as App).locator.mainWindow = new MainWindow(roleOfUser);
                    (Application.Current as App).locator.mainWindow.Activate();
                    this.Close();
                }
                else
                {
                    await new ContentDialog()
                    {
                        XamlRoot = this.Content.XamlRoot,
                        Content = "Invalid username or password",
                        CloseButtonText = "OK"
                    }.ShowAsync();
                }
            }
            else
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "Account is not exist",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }
    }
}
