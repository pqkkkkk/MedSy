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
using Windows.Media.Effects;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views.Doctor
{
    public sealed partial class OnlineConsultationUC : UserControl
    {
        public delegate void EventHandler();
        public event EventHandler ReturnConsultationRequestsEvent;
        private ConsultationRequestsViewModel consultationRequestsViewModel;
        public OnlineConsultationUC(ConsultationRequestsViewModel consultationRequestsViewModel)
        {
            this.InitializeComponent();
            this.consultationRequestsViewModel = consultationRequestsViewModel;
            (Application.Current as App).locator.socketService.endCallMessageReceived += displayEndCallButton;
        }
        private void displayEndCallButton()
        {
            (Application.Current as App).locator.mainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                videlcall = null;
                mainField.Children.Clear();

                endOCButton.Visibility = Visibility.Visible;
                endOCButton.IsEnabled = true;
                endOCMessage.Visibility = Visibility.Visible;
                endOCText.Text = "Return to consultation request page";
            });
        }
        private void endOnlineConsultation(object sender, RoutedEventArgs e)
        {          
            ReturnConsultationRequestsEvent?.Invoke();
        }
        private async void sendDataToVideoCallClientAsync(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
        {
            TimeOnly endTime = consultationRequestsViewModel.nextConsultationToday.endTime;
            TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
            int consultationDuration = (int)endTime.ToTimeSpan().Subtract(now.ToTimeSpan()).TotalSeconds;
            int senderId = (Application.Current as App).locator.currentUser.id;
            int receiverId = consultationRequestsViewModel.nextConsultationToday.patientId;

            string script = $"window.receiveDataFromUserClient({senderId},{receiverId},{consultationDuration});";
            await videlcall.ExecuteScriptAsync(script);
        }
    }
}
