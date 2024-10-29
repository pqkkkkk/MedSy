using MedSy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Patient
{
    public interface IPatientDao
    {
        public Models.Patient getPatient();
    }
}
