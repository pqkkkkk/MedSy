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
        public Models.User getUserByUsername(string username)
        {
            Models.User user = new Models.User()
            {
                id = 1,
                username = "pqkiet854",
                password = "pqkiet854",
                role = "patient",
                avatarPath = "",
                newMessage = false
            };

            return user;
        }
    }
}
