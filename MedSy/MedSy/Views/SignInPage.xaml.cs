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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInPage : Page
    {
        
        public SignInPage()
        {
            this.InitializeComponent();
            
        }

        private async void SignInClicked(object sender, RoutedEventArgs e)
        {
           

            var username = usernameBox.Text;
            var password = passwordBox.Password;
            var actualUsername = (Application.Current as App).locator.patientDao.getPatient().username;
            var actualPassword = (Application.Current as App).locator.patientDao.getPatient().password;

            if (username == actualUsername && password == actualPassword)
            {
                int userId = (Application.Current as App).locator.patientDao.getPatient().id;
                await (Application.Current as App).locator.socketService.connectAsync();
                await (Application.Current as App).locator.socketService.register(userId);

                (Application.Current as App).locator.mainWindow.contentFrame.Navigate(typeof(UserMainPage));
            }
        }
    }
}
