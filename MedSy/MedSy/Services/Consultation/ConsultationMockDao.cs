using ABI.Windows.ApplicationModel.Activation;
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
                    status = "Accepted",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,13),
                    startTime = new TimeOnly(5,00),
                    endTime = new TimeOnly(6,00)
                },
                new Models.Consultation()
                {
                    id = 2,
                    patientId = 1,
                    doctorId = 2,
                    status = "New",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,13),
                    startTime = new TimeOnly(12,00),
                    endTime = new TimeOnly(13,00)
                },
                new Models.Consultation()
                {
                    id = 3,
                    patientId = 1,
                    doctorId = 2,
                    status = "Accepted",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,13),
                    startTime = new TimeOnly(10,00),
                    endTime = new TimeOnly(11,00)
                },
                new Models.Consultation()
                {
                    id = 4,
                    patientId = 1,
                    doctorId = 2,
                    status = "Done",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,16),
                    startTime = new TimeOnly(5,00),
                    endTime = new TimeOnly(6,00)
                },
                new Models.Consultation()
                {
                    id = 5,
                    patientId = 1,
                    doctorId = 2,
                    status = "New",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,16),
                    startTime = new TimeOnly(11,00),
                    endTime = new TimeOnly(12,00)
                },
                new Models.Consultation()
                {
                    id = 5,
                    patientId = 1,
                    doctorId = 3,
                    status = "Accepted",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,15),
                    startTime = new TimeOnly(5,00),
                    endTime = new TimeOnly(6,00)
                },
                new Models.Consultation()
                {
                    id = 5,
                    patientId = 1,
                    doctorId = 3,
                    status = "Done",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,16),
                    startTime = new TimeOnly(5,00),
                    endTime = new TimeOnly(6,00)
                },

            };
        }
        public List<Models.Consultation> GetConsultations(int doctorId, string status, DateOnly? date, TimeOnly? startTime, TimeOnly? endTime)
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
            if(startTime.HasValue)
            {
                result = from c in result
                         where c.startTime >= startTime
                         select c;
            }
            if (endTime.HasValue)
            {
                result = from c in result
                         where c.endTime <= endTime
                         select c;
            }

            return result.ToList();
        }
        public List<Models.Consultation> GetConsultationsInAWeek(int doctorId, DateOnly? date)
        {
            if (date.HasValue)
            {
                int dayOfWeek = (int)date.Value.DayOfWeek;
                DateOnly sunday = date.Value.AddDays(-dayOfWeek);
                DateOnly saturday = sunday.AddDays(6);

                var result = from c in consultations
                             where c.date >= sunday && c.date <= saturday && c.doctorId == doctorId && c.status == "Accepted"
                             select c;

                return result.ToList(); 
;           }
            return null;
        }

        public bool createConsultation(DateOnly? date, TimeOnly? startTime, TimeOnly? endTime, String form, String status, int patientId, int doctorId, String consultation_result, String reason) {
            return true;
        }
    }
}
