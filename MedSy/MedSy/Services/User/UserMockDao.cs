using MedSy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.User
{
    public class UserMockDao : IUserDao
    {
        private List<Models.User> users;
        
        public UserMockDao()
        {
            users = new List<Models.User>()
            {
                new Models.User()
                {
                    id = 1,
                    fullName = "Pham Quoc Kiet",
                    username = "pqkiet854",
                    password = "pqkiet854",
                    avatarPath = "ms-appx:///Assets/avt01.jpg",
                    role = "patient"
                },
                new Models.User()
                {
                    id = 2,
                    fullName="John Doe",
                    username = "johndoe",
                    password = "pqkiet854",
                    avatarPath = "ms-appx:///Assets/doctoravt.jpg",
                    role = "doctor"
                },
                new Models.User()
                {
                    id = 3,
                    fullName = "Lionel Messi",
                    username = "lionelmessi",
                    password = "pqkiet854",
                    avatarPath = "ms-appx:///Assets/doctoravt.jpg",
                    role = "doctor"
                },

            };
        }
        public List<Models.User> getAllUsers()
        {
            return users;
        }
        public Models.User getUserByUsername(string username)
        {
            Models.User user = users.FirstOrDefault(u => u.username == username);

            return user;
        }
    }
}
