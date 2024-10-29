using MedSy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Doctor
{
    public interface IDoctorDao
    {
        
        public List<Models.Doctor> getDoctors();
    }
}
