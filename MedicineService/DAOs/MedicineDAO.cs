using MedicineService.Models;

namespace MedicineService.DAOs
{
    public class MedicineDAO
    {
        public static List<Medicine> GetMedicines()
        {
            List<Medicine> medicines = new List<Medicine>();
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    var medicineList = context.Medicines.ToList();
                    foreach (var medicine in medicineList)
                    {
                        medicines.Add(medicine);
                    }
                }
                return medicines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Medicine GetMedicineById(int id)
        {
            var medicine = new Medicine();
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    medicine = context.Medicines.FirstOrDefault(m => m.MedicineId == id);
                }
                return medicine;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Medicine CreateMedicine(Medicine medicine)
        {
            var createMedicine = new Medicine();
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    var medicineCheck = context.Medicines.FirstOrDefault(m => m.MedicineName == medicine.MedicineName);
                    if (medicineCheck != null)
                    {
                        return null;
                    }
                    else
                    {
                        createMedicine = medicine;
                        createMedicine.IsActive = true;
                        context.Medicines.Add(createMedicine);
                        context.SaveChanges();
                    }
                }
                return medicine;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Medicine UpdateMedicine(Medicine medicine)
        {
            var updatedMedicine = new Medicine();
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    var medicineCheck = context.Medicines.FirstOrDefault(m => m.MedicineId == medicine.MedicineId);
                    if (medicineCheck != null)
                    {
                        updatedMedicine = medicine;
                        updatedMedicine.IsActive = medicineCheck.IsActive;
                        updatedMedicine.UserId = medicineCheck.UserId;
                        context.Entry(medicineCheck).CurrentValues.SetValues(updatedMedicine);
                        context.SaveChanges();
                    }
                    else
                    {
                        return null;
                    }
                }
                return updatedMedicine;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Medicine DeleteMedicine(int id)
        {
            var deletedMedicine = new Medicine();
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    var medicineCheck = context.Medicines.FirstOrDefault(m => m.MedicineId == id);
                    if (medicineCheck != null)
                    {
                        deletedMedicine = medicineCheck;
                        deletedMedicine.IsActive = false;
                        context.Medicines.Update(deletedMedicine);
                        context.SaveChanges();
                    }
                    else
                    {
                        return null;
                    }
                }
                return deletedMedicine;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
