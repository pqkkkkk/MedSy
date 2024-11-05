using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
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
                    id = 3,
                    doctorId = 2,
                    patientId = 1,
                    doctorNewMessage = true,
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
            users = new List<Models.User>()
            {
                new Models.User()
                {
                    id = 1,
                    username = "pqkiet854",
                    password = "pqkiet854",
                    avatarPath = "ms-appx:///Assets/avt01.jpg",
                    role = "patient"
                },
                new Models.User()
                {
                    id = 2,
                    username = "John Doe",
                    password = "pqkiet854",
                    avatarPath = "ms-appx:///Assets/doctoravt.jpg",
                    role = "doctor"
                },
                new Models.User()
                {
                    id = 3,
                    username = "Lionel Messi",
                    password = "pqkiet854",
                    avatarPath = "ms-appx:///Assets/doctoravt.jpg",
                    role = "doctor"
                },

            };
        }
        public List<Models.User> getConnectingUsers(int currentUserId, string currentRole)
        {
            
            var result = from m in managements
                         join u in users on m.doctorId equals u.id
                         where m.patientId == currentUserId
                         select new Doctor()
                         {
                             id = u.id,
                             username = u.username,
                             avatarPath = u.avatarPath,
                             newMessage = m.doctorNewMessage
                         };

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
    }
}
