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
                id = 1, 
                fullName = "Le Van A", 
                speciality = "Cardiology", 
                gender = "Male" , 
                experienceYear= 2, 
                email = "abc@gmail.com",
                address="road 1", 
                phoneNumber = "0354747227",
                birthday = new DateTime(2004,1,1),
                avatarPath="ms-appx:///Assets/doctoravt.jpg",
            },
            new Doctor { 
                id = 2, 
                fullName = "Tran Thi B", 
                speciality = "Neurology",
                gender = "Female" , 
                experienceYear= 1,
                email = "xyz@gmail.com",
                address="road 2",
                 phoneNumber = "0354747227",
                birthday = new DateTime(2004,2,2),
                avatarPath="ms-appx:///Assets/doctoravt.jpg",
            },


            new Doctor { 
                id = 3, 
                fullName = "Nguyen Van C",
                speciality = "Pediatrics", 
                gender = "Male", 
                experienceYear= 5,
                email = "abx@gmail.com",
                address="road 3",
                 phoneNumber = "0354747227",
                birthday = new DateTime(2004,3,3),
                avatarPath="ms-appx:///Assets/doctoravt.jpg",
            },


            new Doctor { 
                id = 4, 
                fullName = "Le Thi D", 
                speciality = "Dermatology", 
                gender = "Female", 
                experienceYear= 1,
                email = "abc@gmail.com",
                address="road 4",
                 phoneNumber = "0354747227",
                birthday = new DateTime(2004,4,4),
                avatarPath="ms-appx:///Assets/doctoravt.jpg",
            },
            new Doctor { id = 5, fullName = "Le Van E", speciality = "Cardiology", gender = "Male" , experienceYear= 2, avatarPath="ms-appx:///Assets/doctoravt.jpg", phoneNumber = "0354747227",},
            new Doctor { id = 6, fullName = "Tran Thi F", speciality = "Neurology", gender = "Female" , experienceYear= 1, avatarPath = "ms-appx:///Assets/doctoravt.jpg",  phoneNumber = "0354747227",},
            new Doctor { id = 7, fullName = "Nguyen Van G", speciality = "Pediatrics", gender = "Male", experienceYear= 5, avatarPath = "ms-appx:///Assets/doctoravt.jpg",  phoneNumber = "0354747227",},
            new Doctor { id = 8, fullName = "Le Thi H", speciality = "Dermatology", gender = "Female", experienceYear= 1, avatarPath = "ms-appx:///Assets/doctoravt.jpg",  phoneNumber = "0354747227",},
            new Doctor { id = 9, fullName = "Le Van I", speciality = "Cardiology", gender = "Male" , experienceYear= 2, avatarPath="ms-appx:///Assets/doctoravt.jpg", phoneNumber = "0354747227",},
            new Doctor { id = 10, fullName = "Tran Thi J", speciality = "Neurology", gender = "Female" , experienceYear= 1, avatarPath = "ms-appx:///Assets/doctoravt.jpg", phoneNumber = "0354747227",},
            new Doctor { id = 11, fullName = "Nguyen Van K", speciality = "Pediatrics", gender = "Male", experienceYear= 5 , avatarPath = "ms-appx:///Assets/doctoravt.jpg",  phoneNumber = "0354747227",},
            new Doctor { id = 12, fullName = "Le Thi L", speciality = "Dermatology", gender = "Female", experienceYear= 1, avatarPath = "ms-appx:///Assets/doctoravt.jpg",  phoneNumber = "0354747227",}
        };

        var query = Doctors.AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(d => d.fullName.Contains(keyword, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(specialty))
            query = query.Where(d => d.speciality == specialty);

        if (!string.IsNullOrEmpty(gender))
            query = query.Where(d => d.gender == gender);

        if (experienceYear > 0)
            query = query.Where(d => d.experienceYear == experienceYear);

        // Phân trang và trả về kết quả
        var result = query.Skip((page - 1) * rowsPerPage).Take(rowsPerPage).ToList();
        return new Tuple<List<Doctor>, int>(result, query.Count());
    }
}

