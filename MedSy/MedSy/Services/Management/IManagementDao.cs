using MedSy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Management
{
    public interface IManagementDao
    {
        public List<Models.User> getConnectingUsers(int currentUserId, string currentRole);
        public void offNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole);
        public void onNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole);
        public void onMySelfNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole);
        public int checkNewMessage(int currentUserId, string currentRole);
    }
}
