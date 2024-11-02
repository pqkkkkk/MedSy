using MedSy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Message
{
    public interface IMessageDao
    {
        public List<Models.Message> getMessages(int patientId, int doctorId);
        public int addMessage(int senderId, int receiverId, string content);
    }
}
