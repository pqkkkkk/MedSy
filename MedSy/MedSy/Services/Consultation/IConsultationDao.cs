using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Consultation
{
    public interface IConsultationDao
    {
        public List<Models.Consultation> GetConsultations(int doctorId, string status, DateTime? date);
    }
}
