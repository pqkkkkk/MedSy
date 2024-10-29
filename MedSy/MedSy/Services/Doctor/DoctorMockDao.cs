using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Doctor
{
    class DoctorMockDao : IDoctorDao
    {
        public Models.Doctor getDoctor()
        {
            Models.Doctor doctor = new Models.Doctor()
            {
                id = 2,
                username ="doctor1",
                password ="pqkiet854",
                avatarPath = "ms-appx:///Assets/doctoravt.jpg"
            };

            return doctor;
        }
        public List<Models.Doctor> getDoctors()
        {
            var result = new List<Models.Doctor>()
            {
                new Models.Doctor()
                {
                    id = 2,
                    username ="doctor1",
                    password ="pqkiet854",
                    avatarPath = "ms-appx:///Assets/doctoravt.jpg"
                },
                new Models.Doctor()
                {
                    id = 3,
                    username ="doctor2",
                    password ="pqkiet854",
                    avatarPath = "ms-appx:///Assets/doctoravt.jpg"
                }
            };

            return result;
        }
    }
}
