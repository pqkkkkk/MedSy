using MedSy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Management
{
    public interface IManagementDao
    {
        public List<Models.Doctor> getDoctors(int patientId);
        public void offDoctorNewMessageNotify(int patientId, int doctorId);
        public void onDoctorNewMessageNotify(int patientId, int doctorId);
    }
}
