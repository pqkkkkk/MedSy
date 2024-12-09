using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Prescription
{
    public interface IPrescriptionDao
    {
        public bool createPrescription(int totalprice, DateOnly createdDay, int consultationId);
        public int getPrescriptionId_ByConsultationId(int consultationId);
        public void insertIntoPrescriptionDetail(int quantity, string usage, int prescriptionId, int drugId);

        public int updateTotalPrice(int totalprice, int prescriptionId);
    }
}
