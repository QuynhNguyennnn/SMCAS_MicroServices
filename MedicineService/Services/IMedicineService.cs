using MedicineService.Models;

namespace MedicineService.Services
{
    public interface IMedicineService
    {
        List<Medicine> GetMedicines();
        Medicine GetMedicineById(int id);
        Medicine CreateMedicine(Medicine medicine);
        Medicine UpdateMedicine(Medicine medicine);
        Medicine DeleteMedicine(int id);
    }
}
