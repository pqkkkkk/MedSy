using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using MedSy.Services.User;
namespace MedSy.Services.Management
{
    public class ManagementMockDao:IManagementDao
    {
        private List<Models.Management> managements;
        private List<Models.User> users;
        public ManagementMockDao()
        {
            managements = new List<Models.Management>()
            {
                new Models.Management()
                {
                    id = 1,
                    doctorId = 2,
                    patientId = 4,
                    doctorNewMessage = false,
                    patientNewMessage = false,
                },
                new Models.Management()
                {
                    id = 3,
                    doctorId = 2,
                    patientId = 1,
                    doctorNewMessage = false,
                    patientNewMessage = false,
                },
                new Models.Management()
                {
                    id = 4,
                    doctorId = 3,
                    patientId = 1,
                    doctorNewMessage = false,
                    patientNewMessage = false,
                },
            };
            IUserDao userDao = new UserMockDao();
            users = userDao.getAllUsers();
        }
        public List<Models.User> getConnectingUsers(int currentUserId, string currentRole)
        {
            IEnumerable<Models.User> result = Enumerable.Empty<Models.User>();
            if (currentRole == "doctor")
            {
                result = from m in managements
                         join u in users on m.patientId equals u.id
                         where m.doctorId == currentUserId
                         select u;
            }
            else if (currentRole == "patient")
            {
                 result = from m in managements
                            join u in users on m.doctorId equals u.id
                            where m.patientId == currentUserId
                            select new Doctor()
                            {
                                id = u.id,
                                username = u.username,
                                avatarPath = u.avatarPath,
                                newMessage = m.doctorNewMessage
                            };
            }
            return result.ToList<Models.User>();
        }
        public void offNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole)
        {
            var management = managements.FirstOrDefault(m =>
            (currentRole == "doctor" && m.doctorId == currentUserId && m.patientId == oppositeUserId) ||
            (currentRole == "patient" && m.patientId == currentUserId && m.doctorId == oppositeUserId));

            if (management != null)
            {
                if (currentRole == "doctor")
                {
                    management.patientNewMessage = false;
                }
                else if (currentRole == "patient")
                {
                    management.doctorNewMessage = false;
                }
            }
        }
        public void onNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole)
        {
            var management = managements.FirstOrDefault(m =>
            (currentRole == "doctor" && m.doctorId == currentUserId && m.patientId == oppositeUserId) ||
            (currentRole == "patient" && m.patientId == currentUserId && m.doctorId == oppositeUserId));

            if (management != null)
            {
                if (currentRole == "doctor")
                {
                    management.patientNewMessage = true;
                }
                else if (currentRole == "patient")
                {
                    management.doctorNewMessage = true;
                }
            }
        }
        public int checkNewMessage(int currentUserId, string currentRole)
        {
            int result = 0;

            if (currentRole == "doctor")
            {
                result = managements.Count(m => m.doctorId == currentUserId && m.patientNewMessage == true);
            }
            else if (currentRole == "patient")
            {
                result = managements.Count(m => m.patientId == currentUserId && m.doctorNewMessage == true);
            }

            return result;
        }

        public void onMySelfNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole)
        {
            throw new NotImplementedException();
        }
    }
}
