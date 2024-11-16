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
                    status = "New",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,13,14,30,30)
                },
                new Models.Consultation()
                {
                    id = 2,
                    patientId = 1,
                    doctorId = 3,
                    status = "New",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,13,14,30,30)
                },
                new Models.Consultation()
                {
                    id = 3,
                    patientId = 1,
                    doctorId = 2,
                    status = "Accepted",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,13,14,30,30)
                },
                new Models.Consultation()
                {
                    id = 4,
                    patientId = 1,
                    doctorId = 2,
                    status = "Done",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,13,14,30,30)
                },
                new Models.Consultation()
                {
                    id = 5,
                    patientId = 1,
                    doctorId = 2,
                    status = "New",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,13,14,30,30)
                },
                new Models.Consultation()
                {
                    id = 5,
                    patientId = 1,
                    doctorId = 3,
                    status = "Accepted",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,15,14,30,30)
                },
                new Models.Consultation()
                {
                    id = 5,
                    patientId = 1,
                    doctorId = 3,
                    status = "Done",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateTime(2024, 11,16,14,30,30)
                },

            };
        }
        public List<Models.Consultation> GetConsultations(int doctorId, string status, DateTime? date)
        {
            var result = from c in consultations
                         where c.doctorId == doctorId
                         select c;

            if(status != null && status != "All")
            {
                result = from c in result
                         where c.status == status
                         select c;
            }
            if (date.HasValue)
            {
                result = from c in result
                         where c.date.Year == date.Value.Year && c.date.Month == date.Value.Month
                         && c.date.Day == date.Value.Day
                         select c;
            }

            return result.ToList();
        }
    }
}
