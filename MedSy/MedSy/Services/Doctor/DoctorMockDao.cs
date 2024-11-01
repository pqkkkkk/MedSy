using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
namespace MedSy;
public class DoctorMockDao : IDoctorDao
{
    public Tuple<List<Doctor>, int> GetDoctors(int page, int rowsPerPage,string keyword, string specialty, string gender, int experienceYear)
    {
        var Doctors = new List<Doctor>
        {
            new Doctor 
            { 
                DoctorID = "1", 
                DoctorName = "Le Van A", 
                Specialty = "Cardiology", 
                Gender = "Male" , 
                ExperienceYear= 2, 
                Email = "abc@gmail.com",
                Address="road 1", 
                PhoneNumber = "0354747227",
                Birthday = new DateTime(2004,1,1),
            },
            new Doctor { 
                DoctorID = "2", 
                DoctorName = "Tran Thi B", 
                Specialty = "Neurology",
                Gender = "Female" , 
                ExperienceYear= 1,
                Email = "xyz@gmail.com",
                Address="road 2",
                Birthday = new DateTime(2004,2,2),},

            new Doctor { 
                DoctorID = "3", 
                DoctorName = "Nguyen Van C",
                Specialty = "Pediatrics", 
                Gender = "Male", 
                ExperienceYear= 5,
                Email = "abx@gmail.com",
                Address="road 3",
                Birthday = new DateTime(2004,3,3),},

            new Doctor { 
                DoctorID = "4", 
                DoctorName = "Le Thi D", 
                Specialty = "Dermatology", 
                Gender = "Female", 
                ExperienceYear= 1,
                Email = "abc@gmail.com",
                Address="road 4",
                Birthday = new DateTime(2004,4,4),},
            new Doctor { DoctorID = "5", DoctorName = "Le Van E", Specialty = "Cardiology", Gender = "Male" , ExperienceYear= 2},
            new Doctor { DoctorID = "6", DoctorName = "Tran Thi F", Specialty = "Neurology", Gender = "Female" , ExperienceYear= 1},
            new Doctor { DoctorID = "7", DoctorName = "Nguyen Van G", Specialty = "Pediatrics", Gender = "Male", ExperienceYear= 5 },
            new Doctor { DoctorID = "8", DoctorName = "Le Thi H", Specialty = "Dermatology", Gender = "Female", ExperienceYear= 1},
            new Doctor { DoctorID = "9", DoctorName = "Le Van I", Specialty = "Cardiology", Gender = "Male" , ExperienceYear= 2},
            new Doctor { DoctorID = "10", DoctorName = "Tran Thi J", Specialty = "Neurology", Gender = "Female" , ExperienceYear= 1},
            new Doctor { DoctorID = "11", DoctorName = "Nguyen Van K", Specialty = "Pediatrics", Gender = "Male", ExperienceYear= 5 },
            new Doctor { DoctorID = "12", DoctorName = "Le Thi L", Specialty = "Dermatology", Gender = "Female", ExperienceYear= 1}
        };

        var query = Doctors.AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(d => d.DoctorName.Contains(keyword, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(specialty))
            query = query.Where(d => d.Specialty == specialty);

        if (!string.IsNullOrEmpty(gender))
            query = query.Where(d => d.Gender == gender);

        if (experienceYear > 0)
            query = query.Where(d => d.ExperienceYear == experienceYear);

        // Phân trang và trả về kết quả
        var result = query.Skip((page - 1) * rowsPerPage).Take(rowsPerPage).ToList();
        return new Tuple<List<Doctor>, int>(result, query.Count());
    }
}

