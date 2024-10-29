using MedSy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Patient
{
    class PatientMockDao : IPatientDao
    {
        public Models.Patient getPatient()
        {
            Models.Patient patient = new Models.Patient()
            {
                id = 3,
                username = "pqkiet",
                password = "pqkiet854",
                avatarPath = "ms-appx:///Assets/avt01.jpg"
            };

            return patient;
        }
    }
}
