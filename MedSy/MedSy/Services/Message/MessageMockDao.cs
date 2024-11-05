using MedSy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Text;

namespace MedSy.Services.Message
{
    public class MessageMockDao : IMessageDao
    {
        private List<Models.Message> messages;
        public MessageMockDao()
        {
            messages = new List<Models.Message>()
            {
                new Models.Message()
                {
                    content = "Hello",
                    senderId = 2,
                    receiverId = 1
                },
                new Models.Message()
                {
                    content = "How can I help you?",
                    senderId = 2,
                    receiverId = 1
                },
                new Models.Message()
                {
                    content = "Thanks",
                    senderId = 1,
                    receiverId = 2
                },
                new Models.Message()
                {
                    content = "How can I help you?",
                    senderId = 3,
                    receiverId = 1
                },
                new Models.Message()
                {
                    content = "Hello",
                    senderId = 3,
                    receiverId = 1
                },
                new Models.Message()
                {
                    content = "Thanks",
                    senderId = 1,
                    receiverId = 3
                }
            };
        }
        public List<Models.Message> getMessages(int currentUserId, int oppositeUserId)
        {
            

            var result = from m in messages
                         where (m.senderId == currentUserId && m.receiverId == oppositeUserId)
                               || (m.senderId == oppositeUserId && m.receiverId == currentUserId)
                         select m;

            return result.ToList();
        }
        public int addMessage(int senderId, int receiverId, string content)
        {
            var message = new Models.Message()
            {
                senderId = senderId,
                receiverId = receiverId,
                content = content
            };
            messages.Add(message);

            return 1;
        }
    }
}
