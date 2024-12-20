using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MedSy.Services
{
    public class SocketService
    {
        private SocketIOClient.SocketIO socket;
        public event EventHandler<Tuple<string,int>> MessageReceived;
        public delegate void endCallMessageEventHandler();
        public event endCallMessageEventHandler endCallMessageReceived;
        public delegate void acceptedCRNotiEventHandler();
        public event acceptedCRNotiEventHandler acceptedCRNotiReceived;
        public delegate void offerEventHandler();
        public event offerEventHandler offerReceived;
        public delegate void newCREventHandler();
        public event newCREventHandler newCRReceived;
        public delegate void isLoadingEventHandler(bool isShow);
        public event isLoadingEventHandler isLoading;
        public delegate void PaymentCompleteMessageEventHandler();
        public event PaymentCompleteMessageEventHandler paymentCompleteMessageReceived;
        public SocketService()
        {
            socket = new SocketIOClient.SocketIO("http://localhost:5555");
        }
        public async Task<int> connectAsync()
        {
            isLoading?.Invoke(true);
            var tcs = new TaskCompletionSource<int>();

            try
            {
                socket.OnConnected -= OnConnectedHandler;
                socket.OnError -= OnErrorHandler;
                socket.OnDisconnected -= OnDisconnectedHandler;

                socket.OnConnected += OnConnectedHandler;
                socket.OnError += OnErrorHandler;
                socket.OnDisconnected += OnDisconnectedHandler;

                socket.Options.Reconnection = false;
                socket.Options.ConnectionTimeout = TimeSpan.FromSeconds(1);
                await socket.ConnectAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                if (!tcs.Task.IsCompleted)
                    tcs.TrySetResult(0);
            }

            return await tcs.Task;

            void OnConnectedHandler(object sender, EventArgs e)
            {
                if (!tcs.Task.IsCompleted)
                {
                    tcs.TrySetResult(1);
                    receiveMessage();
                    receiveEndCallMessage();
                    receiveNewCRMessage();
                    receiveAcceptedCRNoti();
                    receivePaymentCompleteMessage();
                }
            }
            void OnErrorHandler(object sender, string e)
            {
                if (!tcs.Task.IsCompleted)
                {
                    Console.WriteLine("Error: " + e);
                    tcs.TrySetResult(0);
                }
            }
            void OnDisconnectedHandler(object sender, string e)
            {
                if (!tcs.Task.IsCompleted)
                {
                    Console.WriteLine("Disconnected: " + e);
                    tcs.TrySetResult(0);
                }
            }
        }
        public async Task register(int userId)
        {
            string role = "user";
            await socket.EmitAsync("register", new { userId, role  });
        }
        public void receiveMessage()
        {
            var tcs = new TaskCompletionSource<string>();
            string message = "";
            int senderId = 0;
           
            socket.On("messageFromServer", response =>
            {
              
                var serverData = response.GetValue<JsonObject>();
                message = serverData["message"].ToString();
                senderId = int.Parse(serverData["senderId"].ToString());

                var data = new Tuple<string, int>(
                    message,
                    senderId
                    );
                MessageReceived?.Invoke(this, data);
                Debug.WriteLine("received message from server");
            });
        }
        public void receiveEndCallMessage()
        {
            socket.On("endCallMessage",response =>
            {
                endCallMessageReceived?.Invoke();
                Debug.WriteLine("Received end call message from corresponding video call client");
            });
        }
        public async Task sendMessage(string message,int senderId, int receiverId)
        {
            await socket.EmitAsync("messageFromClient", new { message, senderId, receiverId });
        }
        public void receiveAcceptedCRNoti()
        {
            socket.On("acceptedCRNoti", response =>
            {
                acceptedCRNotiReceived?.Invoke();
                Debug.WriteLine("Received accepted CR notification");
            });
        }
        public async Task sendAcceptedCRNoti(int senderId,int receiverId)
        {
            await socket.EmitAsync("acceptedCRNoti", new {senderId,receiverId});
        }
        public void sendNewCRMessage(int senderId, int receiverId)
        {
            socket.EmitAsync("newCR", new { senderId, receiverId });
        }
        public void receiveNewCRMessage()
        {
            socket.On("newCR", response =>
            {
                newCRReceived?.Invoke();
            });
        }
        public void receiveOfferMessage()
        {
            socket.On("offer", response =>
            {
                offerReceived?.Invoke();
            });
        }
        public void receivePaymentCompleteMessage()
        {
            socket.On("paymentCompleteMessage", response =>
            {
                paymentCompleteMessageReceived?.Invoke();
            });
        }
    }
}
