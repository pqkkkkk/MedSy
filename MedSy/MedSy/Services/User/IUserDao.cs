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
        public Models.User getUserById(int id);
        public Tuple<List<Doctor>, int> GetDoctors(int page, int rowsPerPage, string keyword, string specialty, string gender, int experienceYear);
    }
}
