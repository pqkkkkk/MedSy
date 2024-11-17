using MedSy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.User
{
    public interface IUserDao
    {
        public Models.User getUserByUsername(string username);
        public List<Models.User> getAllUsers();
    }
}
