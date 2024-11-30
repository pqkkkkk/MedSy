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
        public SocketService()
        {
            socket = new SocketIOClient.SocketIO("http://localhost:5555");
        }
        public async Task<int> connectAsync()
        {
            var tcs = new TaskCompletionSource<int>();

            socket.OnConnected +=  (sender, e) =>
            {
                tcs.SetResult(1);
                receiveMessage();
                receiveEndCallMessage();
            };
            socket.OnError += (sender, e) =>
            {
                tcs.SetResult(0);
            };

            await socket.ConnectAsync();
            return await tcs.Task;
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
            });
        }
        public async Task sendAcceptedCRNoti(int senderId,int receiverId)
        {
            await socket.EmitAsync("acceptedCRNoti", new {senderId,receiverId});
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

    }
}
