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
        public List<Models.Message> getMessages(int patientId, int doctorId)
        {
            var messages = new List<Models.Message>()
            {
                new Models.Message()
                {
                    content = "Hello",
                    senderId = 1,
                    receiverId = 2
                },
                new Models.Message()
                {
                    content = "How can I help you?",
                    senderId = 1,
                    receiverId = 2
                },
                new Models.Message()
                {
                    content = "kkkkk",
                    senderId = 1,
                    receiverId = 2
                },
                new Models.Message()
                {
                    content = "How can I help you?",
                    senderId = 1,
                    receiverId = 3
                },
                new Models.Message()
                {
                    content = "Hello",
                    senderId = 1,
                    receiverId = 3
                },
                new Models.Message()
                {
                    content = "How can I help you?",
                    senderId = 1,
                    receiverId = 3
                }
            };

            var result = from m in messages
                         where m.senderId == patientId && m.receiverId == doctorId
                         select m;

            return result.ToList();
        }
    }
}
