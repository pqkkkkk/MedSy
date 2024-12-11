using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;

namespace MedSy.Services.Patient
{
    public class PatientMockDao:IPatientDao
    {
        public List<Models.Patient> GetAllPatient() 
        {
            var Patients = new List<Models.Patient>
            {
               new Models.Patient
                {
                    id = 5, // Trùng với User ID của role "patient"
                    fullName = "Pham Quoc Kiet",
                    username = "pqkiet854",
                    password = "pqkiet854",
                    avatarPath = "ms-appx:///Assets/avt01.jpg",
                    address = "123 Main St",
                    email = "pqkiet@example.com",
                    phoneNumber = "0354747227",
                    birthday = new DateTime(1998, 5, 15),
                    gender = "Male",
                    healthInsurance = true
                },
            };
            return Patients;
        }
    }
}
