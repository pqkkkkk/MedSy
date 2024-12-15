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
using System.Text.Json.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Diagnostics;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaymentTest : Page
    {
        public PaymentTest()
        {
            this.InitializeComponent();
        }

        private async void PayButton_Click(object sender, RoutedEventArgs e)
        {
            string payment_url = "http://localhost:5555/create_payment_url";

            var data = new
            {
                amount = 10000,
                orderDescription = "Thanh toan don thuoc",
                orderType = "other",
                language = "vn",
                bankCode = "NCB",
            };
            string jsonData = JsonConvert.SerializeObject(data);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(payment_url, content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<Dictionary<string,object>>(responseContent);
                    Debug.WriteLine($"Phản hồi từ server: {responseContent}");

                    if (responseObject != null && responseObject.ContainsKey("data"))
                    {
                        string paymentUrl = responseObject["data"].ToString();
                        payment.Source = new Uri(paymentUrl);
                        payment.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Lỗi khi gửi yêu cầu: {ex.Message}");
                }
            }


        }
    }
}
