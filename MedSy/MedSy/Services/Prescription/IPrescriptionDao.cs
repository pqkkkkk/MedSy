using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Prescription
{
    public interface IPrescriptionDao
    {
        public bool createPrescription(int totalprice, DateOnly createdDay, int consultationId);
        public int getPrescriptionId_ByConsultationId(int consultationId);
        public void insertIntoPrescriptionDetail(Models.PrescriptionDetail prescriptionDetail, int prescriptionId);
        public List<Models.PrescriptionDetail> getPrescriptionDetails(int consultationId);
        public int updateTotalPrice(int totalprice, int prescriptionId);
        public List<Models.PrescriptionDetail> getPrescriptionDetails_ByPrescriptionId(int prescriptionId);
        public List<Models.Prescription> GetPrescriptions(int userId, string status);
        public int UpdatePrescriptionStatus(int prescriptionId);
        public Dictionary<int, int> calculateRevenueByYear(int year);
        public Dictionary<int, int> calculateRevenueByMonth(int month, int year);
        public Dictionary<int, int> calculateRevenueByWeek(int week, int month, int year);

    }
}
