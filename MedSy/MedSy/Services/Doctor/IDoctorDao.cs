using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;

public interface IDoctorDao
{
    Tuple<List<Doctor>, int> GetDoctors(int page, int rowsPerPage, string keyword, string specialty, string gender, int experienceYear);
}

