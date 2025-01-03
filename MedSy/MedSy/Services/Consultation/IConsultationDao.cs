using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Consultation
{
    public interface IConsultationDao
    {
        public List<Models.Consultation> GetConsultations(string userRole, int userId, string status, DateOnly? date, TimeOnly? startTime, TimeOnly? endTime);
        public List<Models.Consultation> GetAllConsultationsByDoctorIdAndPatientId(int doctorId, int patientId, string status, DateOnly? date, TimeOnly? startTime, TimeOnly? endTime);
        public List<Models.Consultation> GetConsultationsInAWeek(int doctorId,DateOnly? date);
        public int UpdateStatusToDone(Models.Consultation selectedConsultation);
        public int UpdateAllMissedConsultations(string userRole, int userId);
        public Models.Consultation GetNextConsultationToday(string userRole, int userId);
        public bool createConsultation(DateOnly? date, TimeOnly? startTime, TimeOnly? endTime, String form, String status, int patientId, int doctorId, String consultation_result, String reason, string pathology);
        public int UpdateStatus(Models.Consultation selectedConsultation, string status);
        public bool updateResult(int id, string result);
        public int DeleteConsultation(Models.Consultation selectedConsultation);
        public Dictionary<string, int> GetPathologyCountByMonth(int month, int year);
        public Dictionary<string, int> GetPathologyCountByYear(int year);
        public Dictionary<string, int> GetPathologyCountByWeek(int week, int month, int year);
        public Dictionary<int, int> countOnlineConsultationByYear(int year);
        public Dictionary<int, int> countOnlineConsultationByMonth(int month, int year);
        public Dictionary<int, int> countOnlineConsultationByWeek(int week, int month, int year);
        public List<string> getAllPathology();
    }
}
