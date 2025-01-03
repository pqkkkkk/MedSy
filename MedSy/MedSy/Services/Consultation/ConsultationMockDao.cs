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
                    date = new DateOnly(2025, 01,03),
                    startTime = new TimeOnly(17,00),
                    endTime = new TimeOnly(18,00)
                },
                new Models.Consultation()
                {
                    id = 2,
                    patientId = 1,
                    doctorId = 2,
                    status = "New",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2025, 1,3),
                    startTime = new TimeOnly(17,00),
                    endTime = new TimeOnly(18,00)
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
                    id = 6,
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
                    id = 7,
                    patientId = 1,
                    doctorId = 3,
                    status = "Done",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,16),
                    startTime = new TimeOnly(5,00),
                    endTime = new TimeOnly(6,00)
                },
                new Models.Consultation()
                {
                    id = 8,
                    patientId = 1,
                    doctorId = 3,
                    status = "Accepted",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,27),
                    startTime = new TimeOnly(17,00),
                    endTime = new TimeOnly(18,00)
                },
                new Models.Consultation()
                {
                    id = 9,
                    patientId = 4,
                    doctorId = 2,
                    status = "Done",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,27),
                    startTime = new TimeOnly(12,00),
                    endTime = new TimeOnly(13,00)
                },
                new Models.Consultation()
                {
                    id = 10,
                    patientId = 4,
                    doctorId = 3,
                    status = "Done",
                    result = "",
                    reason ="kkkkkkkk",
                    date = new DateOnly(2024, 11,27),
                    startTime = new TimeOnly(12,00),
                    endTime = new TimeOnly(13,00)
                },

            };
        }
        public Dictionary<string, int> GetPathologyCountByMonth(int month)
        {
            return new Dictionary<string, int>();
        }
        public Dictionary<int, int> countOnlineConsultationEachMonth(int year)
        {
            return new Dictionary<int, int>();
        }
        public List<string> getAllPathology()
        {
            return new List<string>();
        }
        public List<Models.Consultation> GetConsultations(string userRole, int userId, string status, DateOnly? date, TimeOnly? startTime, TimeOnly? endTime)
        {
            IEnumerable<Models.Consultation> result = Enumerable.Empty<Models.Consultation>();
            if (userRole == "doctor")
            {
                result = (from c in consultations
                          where c.doctorId == userId
                          select c);
            }
            else if (userRole == "patient")
            {
                result = from c in consultations
                         where c.patientId == userId
                         select c;
            }
            else
            {
                result = from c in consultations
                         select c;
            }
            if (status != null && status != "All")
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
            if (startTime.HasValue)
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
                ;
            }
            return null;
        }
        public int UpdateStatusToDone(Models.Consultation selectedConsultation)
        {
            var consultationToUpdate = consultations.FirstOrDefault(c => c.id == selectedConsultation.id);

            if (consultationToUpdate != null)
            {
                consultationToUpdate.status = "Done";
                return 1;
            }
            return 0;
        }
        public Models.Consultation GetNextConsultationToday(string userRole, int userId)
        {
            var list = GetConsultations(userRole, userId, "Accepted", DateOnly.FromDateTime(DateTime.Now), null, null);
            Models.Consultation result = null;
            if (list.Count != 0)
            {
                list.OrderBy(c => c.startTime).ToList();
                result = list.FirstOrDefault();

            }
            return result;

        }
        public bool createConsultation(DateOnly? date, TimeOnly? startTime, TimeOnly? endTime, String form, String status, int patientId, int doctorId, String consultation_result, String reason)
        {
            return true;
        }


        public int UpdateStatus(Models.Consultation selectedConsultation, string status)
        {
            return 1;
        }

        public int DeleteConsultation(Models.Consultation selectedConsultation)
        {
            return 1;
        }
        public bool updateResult(int id, string result)
        {
            return true;
        }

        public int UpdateAllMissedConsultations(string userRole,int userId)
        {
            var list = consultations.Where(c => c.date == DateOnly.FromDateTime(DateTime.Now) && c.endTime < TimeOnly.FromDateTime(DateTime.Now) && c.status == "Accepted").ToList();
            foreach (var c in list)
            {
                c.status = "Missed";
            }
            return 1;
        }

        public List<Models.Consultation> GetAllConsultationsByDoctorIdAndPatientId(int doctorId, int patientId)
        {
            throw new NotImplementedException();
        }

        public bool createConsultation(DateOnly? date, TimeOnly? startTime, TimeOnly? endTime, string form, string status, int patientId, int doctorId, string consultation_result, string reason, string pathology)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, int> GetPathologyCountByMonth(int month, int year)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, int> GetPathologyCountByYear(int year)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, int> GetPathologyCountByWeek(int week, int month, int year)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, int> countOnlineConsultationByYear(int year)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, int> countOnlineConsultationByMonth(int month, int year)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, int> countOnlineConsultationByWeek(int week, int month, int year)
        {
            throw new NotImplementedException();
        }

        public List<Models.Consultation> GetAllConsultationsByDoctorIdAndPatientId(int doctorId, int patientId, string status, DateOnly? date, TimeOnly? startTime, TimeOnly? endTime)
        {
            throw new NotImplementedException();
        }
    }
}

