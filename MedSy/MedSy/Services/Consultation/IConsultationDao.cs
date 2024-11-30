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
        public List<Models.Consultation> GetConsultationsInAWeek(int doctorId,DateOnly? date);
        public int UpdateStatusToDone(Models.Consultation selectedConsultation);
        public Models.Consultation GetNextConsultationToday(string userRole, int userId);
    }
}
