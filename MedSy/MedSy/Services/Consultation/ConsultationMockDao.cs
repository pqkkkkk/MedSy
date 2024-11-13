using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Consultation
{
    public class ConsultationMockDao : IConsultationDao
    {
        private List<Models.Consultation> consultations;
        public ConsultationMockDao()
        {
            consultations = new List<Models.Consultation>()
            {
                new Models.Consultation()
                {
                    id = 1,
                    patientId = 1,
                    doctorId = 2,
                    status = "new",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,13,14,30,30)
                },
                new Models.Consultation()
                {
                    id = 2,
                    patientId = 1,
                    doctorId = 3,
                    status = "new",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,13,14,30,30)
                },
                new Models.Consultation()
                {
                    id = 3,
                    patientId = 1,
                    doctorId = 2,
                    status = "confirmed",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,13,14,30,30)
                },
                new Models.Consultation()
                {
                    id = 4,
                    patientId = 1,
                    doctorId = 2,
                    status = "done",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,13,14,30,30)
                },
                new Models.Consultation()
                {
                    id = 5,
                    patientId = 1,
                    doctorId = 2,
                    status = "new",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,13,14,30,30)
                },

            };
        }
        public List<Models.Consultation> GetConsultations(int doctorId)
        {
            var result = from c in consultations
                         where c.doctorId == doctorId
                         select c;
            return result.ToList();
        }
    }
}
