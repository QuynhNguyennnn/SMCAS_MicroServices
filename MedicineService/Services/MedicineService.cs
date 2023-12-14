using MedicineService.DAOs;
using MedicineService.Models;

namespace MedicineService.Services
{
    public class MedicineService : IMedicineService
    {
        public Medicine CreateMedicine(Medicine Medicine) => MedicineDAO.CreateMedicine(Medicine);

        public Medicine DeleteMedicine(int id) => MedicineDAO.DeleteMedicine(id);

        public Medicine GetMedicineById(int id) => MedicineDAO.GetMedicineById(id);

        public List<Medicine> GetMedicines() => MedicineDAO.GetMedicines();

        public Medicine UpdateMedicine(Medicine Medicine) => MedicineDAO.UpdateMedicine(Medicine);

        public List<Medicine> GetMedicinesByname(string name) => MedicineDAO.SearchMedicineByName(name);
    }
}
