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
        public Tuple<List<Models.Drug>, int> GetDrugs(int page, int rowsPerPage, string keyword, string drugType, double minPrice, double maxPrice);
        public Models.Drug getDrugById(int id);
        public bool addDrug(Models.Drug drug);
    }
}