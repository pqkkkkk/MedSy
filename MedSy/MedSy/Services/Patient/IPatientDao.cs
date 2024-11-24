using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;

namespace MedSy.Services.Patient
{
    public interface IPatientDao
    {
        public List<Models.Patient> GetAllPatient();
    }
}
