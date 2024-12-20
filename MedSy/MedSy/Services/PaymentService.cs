using MedSy.Models;
using Microsoft.UI.Xaml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services
{
    public class PaymentService
    {
        private string createPaymentUrl;
        public PaymentService()
        {
            createPaymentUrl = "http://localhost:5555/create_payment_url";
        }
        public async Task<string> CreatePaymentAsync(int amount, int prescriptionId)
        {
            var tcs = new TaskCompletionSource<string>();
            int currentUserId = (Application.Current as App).locator.currentUser.id;
            string orderId = $"{currentUserId}_{prescriptionId}";
            var data = new
            {
                amount = amount,
                orderDescription = $"Thanh toan don thuoc {prescriptionId}",
                orderId = orderId,
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

                    HttpResponseMessage response = await client.PostAsync(createPaymentUrl, content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);
                    Debug.WriteLine($"Phản hồi từ server: {responseContent}");

                    if (responseObject != null && responseObject.ContainsKey("data"))
                    {
                        string paymentUrl = responseObject["data"].ToString();
                        tcs.TrySetResult(paymentUrl);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Lỗi khi gửi yêu cầu: {ex.Message}");
                    tcs.TrySetResult("");
                }
            }
            return await tcs.Task;
            
        }

    }
}
