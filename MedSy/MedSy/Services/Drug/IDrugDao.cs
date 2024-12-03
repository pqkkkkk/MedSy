using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MedSy.Services.Drug
{
    public interface IDrugDao
    {
        public List<Models.Drug> getAllDrugs(string keyword, string drugType);
        public List<string> getAllDrugType();
        public void updateQuantity(int quantity, int id);
    }
}